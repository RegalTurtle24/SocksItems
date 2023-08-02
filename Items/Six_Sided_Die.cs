using BepInEx.Configuration;
using R2API;
using RoR2;
using RoR2GenericModTemplate1.Base_Classes;
using RoR2GenericModTemplate1.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static RoR2GenericModTemplate1.Utils.ItemHelper;
using SocksNeedsItems;
using UnityEngine.Networking;
using System.Linq;

namespace RoR2GenericModTemplate1.Items
{
    public class Six_Sided_Die : ItemBase
    {
        public override string ItemName => "Six-Sided Die";

        public override string ItemLangTokenName => "sixSidedDie";

        public override string ItemPickupDesc => "EXAMPLE_ITEM";

        public override string ItemFullDescription => "EXAMPLE_ITEM";

        public override string ItemLore => "EXAMPLE_ITEM";

        public override ItemTier Tier => ItemTier.Tier1;

        public override string ItemModelPath => "null";

        public override string ItemIconPath => "six_sided_die.png";

        public override bool CanRemove => true;

        public override bool Hidden => false;

        public override void CreateConfig(ConfigFile config)
        {
            //configs go here
        }

        public override ItemDisplayRuleDict CreateItemDisplayRules()
        {
            ItemBodyModelPrefab = Main.Assets.LoadAsset<GameObject>(ItemModelPath);
            var itemDisplay = ItemBodyModelPrefab.AddComponent<ItemDisplay>();
            itemDisplay.rendererInfos = ItemDisplaySetup(ItemBodyModelPrefab);

            /*ItemDisplayRuleDict rules = new ItemDisplayRuleDict(new RoR2.ItemDisplayRule[]
            {

                new RoR2.ItemDisplayRule
               {
                    ruleType = ItemDisplayRuleType.ParentedPrefab,
                    followerPrefab = ItemBodyModelPrefab,
                    childName = "Chest",
                    localPos = new Vector3(0, 0, 0),
                    localAngles = new Vector3(0, 0, 0),
                    localScale = new Vector3(1, 1, 1)
                }

            });*/

            ItemDisplayRuleDict rules = new ItemDisplayRuleDict(null);

            return rules;
        }

        public override void Hooks()
        {

            /* 15% (+5% per stack, linear) chance when healing for more than 10 % (-1% per stack, caps at 1 %
             to trigger a random on kill effect of one of your help items on the nearest enemy. */
            On.RoR2.HealthComponent.Heal += HealthComponent_Heal;

        }

        private float HealthComponent_Heal(On.RoR2.HealthComponent.orig_Heal orig, HealthComponent self, float amount, ProcChainMask procChainMask, bool nonRegen)
        {
            if (!self.body.inventory)
            {
                return amount;
            }

            var num = GetCount(self.body);
            if (num > 0)
            {
                var percentOfHealth = amount / self.body.maxHealth;
                if (percentOfHealth > Math.Max(0.11 - (num * 0.01), 0.01))
                {
                    if (Util.CheckRoll(15 + (5 * (num - 1)), self.body.GetComponent<CharacterMaster>()))
                    {
                        // find the closest enemy
                        this.bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
                        this.bullseyeSearch.teamMaskFilter.RemoveTeam(self.body.GetComponent<CharacterMaster>().teamIndex);
                        this.bullseyeSearch.filterByLoS = false;
                        this.bullseyeSearch.searchOrigin = self.body.GetComponent<CharacterMaster>().transform.position;
                        this.bullseyeSearch.searchDirection = self.body.GetComponent<CharacterMaster>().transform.forward;
                        this.bullseyeSearch.maxDistanceFilter = 130;
                        this.bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
                        this.bullseyeSearch.maxAngleFilter = 360;
                        this.bullseyeSearch.RefreshCandidates();
                        IEnumerable<HurtBox> enumerable = this.bullseyeSearch.GetResults();
                        HurtBox target = enumerable.FirstOrDefault<HurtBox>();
                        // HealthComponent healthComponent = this.healthComponent;
                        // DamageReport damageReport = new DamageReport(damageInfo, healthComponent, damageInfo.damage, this.healthComponent.combinedHealth);
                        // GlobalEventManager.instance.OnCharacterDeath(damageReport);
                        DamageInfo damageInfo = new DamageInfo
                        {
                            attacker = null,
                            crit = false,
                            damage = 0,
                            position = self.body.GetComponent<CharacterMaster>().transform.position,
                            procCoefficient = 1,
                            damageType = DamageType.Generic,
                            damageColorIndex = DamageColorIndex.Default
                        };
                        DamageReport damageReport = new DamageReport(damageInfo, target.healthComponent, damageInfo.damage, target.healthComponent.combinedHealth);
                        GlobalEventManager.instance.OnCharacterDeath(damageReport);
                    }
                }
            }
            return amount;
        }

        public override void Init(ConfigFile config)
        {
            CreateConfig(config);
            /*CreateItemDisplayRules();*/
            CreateLang();
            CreateItem();
            Hooks();

            this.bullseyeSearch = new BullseyeSearch();
        }

        private BullseyeSearch bullseyeSearch;
    }
}
