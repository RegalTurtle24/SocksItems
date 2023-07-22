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

namespace RoR2GenericModTemplate1.Items
{
    public class Omnis_Rend : ItemBase
    {
        public override string ItemName => "Omni's Rend";

        public override string ItemLangTokenName => "omnisRend";

        public override string ItemPickupDesc => "EXAMPLE_ITEM";

        public override string ItemFullDescription => "EXAMPLE_ITEM";

        public override string ItemLore => "EXAMPLE_ITEM";

        public override ItemTier Tier => ItemTier.Tier1;

        public override string ItemModelPath => "null";

        public override string ItemIconPath => "null";

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

            On.RoR2.HealthComponent.Heal += (orig, self, amount, procChainMask, nonRegen) =>
            {
                float num = amount;
                if (!NetworkServer.active) return num;
                if (GetCount(self.body) > 0 && nonRegen)
                {
                    // what about 1% max health per stack
                    // num += GetCount(self.body) * self.body.maxHealth * (float)0.02;
                    num += 10 * GetCount(self.body);
                }
                return orig(self, num, procChainMask, nonRegen);
            };

        }

        public override void Init(ConfigFile config)
        {
            CreateConfig(config);
            /*CreateItemDisplayRules();*/
            CreateLang();
            CreateItem();
            Hooks();

        }
    }
}
