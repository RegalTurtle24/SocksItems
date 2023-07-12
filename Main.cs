using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2GenericModTemplate1.Base_Classes;
using System;
using System.Reflection;
using UnityEngine;

using RoR2;
using SocksItems;

//automatically renamed based on project name.
namespace RoR2GenericModTemplate1
{
    //--------------R2API dependency. This template is heavily based on the modules provided by this API, so it uses it as a dependency.--------------------
    [BepInDependency(R2API.R2API.PluginGUID, R2API.R2API.PluginVersion)]

    //add other mod dependencies here using the format listed above.

    //------------------Makes mod server-side. Change this only if you're sure it won't fuck something up-----------------------
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]

    //------------------Defines your mod's GUID, Name, and Version to BepIn. These are set in variables later.---------------------------
    [BepInPlugin(ModGUID, ModName, ModVersion)]

    //main class where the most basic stuff happens. Basically only here to activate the mod.
    public class Main : BaseUnityPlugin
    {

        //define mod ID: uses the format of "com.USERNAME.MODNAME"
        public const string ModGUID = "com.RegalTurtle24.SocksItems";

        //define the mod name inside quotes. Can be anything.
        public const string ModName = "SocksItems";

        //define mod version inside quotes. Follows format of "MAJORVERSION.MINORPATCH.BUGFIX". Ex: 1.2.3 is Major Release 1, Patch 2, Bug Fix 3.
        public const string ModVersion = "0.0.0";

        //Creates an asset bundle that can be easily accessed from other classes
        public static AssetBundle Assets;

        //List other necessary variables and bits here. For example, you may need a list of all your new things to add them to the game properly.

        //this method runs when your mod is loaded.
        public void Awake()
        {
            Log.Init(Logger);

            //loads an asset bundle if one exists. Objects will need to be called from this bundle using AssetBundle.LoadAsset(string path)
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RoR2GenericModTemplate1.mod_assets"))
            {

                if (stream != null)
                {

                    Assets = AssetBundle.LoadFromStream(stream);

                }

            }


            //runs our configs
            Configs();

            //this method will instantiate everything we want to add to the game. see below
            Instantiate();

            //runs hooks that are seperate from all additions (i.e, if you need to call something when the game runs or at special times)
            Hooks();

        }

        public void Configs()
        {

            //insert configs here

        }

        public void Hooks()
        {

            //insert hooks here

        }

        //we make calls to Verify on each thing here to make our call in Awake clean
        public void Instantiate()
        {
            VerifyItems(new Items.Omnis_Rend());
            /*VerifyAchievements(new Examples.EXAMPLE_ACHIEVEMENT());*/

        }

        //this method will instantiate our items based on a generated config option
        public void VerifyItems(ItemBase item)
        {
            //generates a config file to turn the item on or off and get its value
            /*var isEnabled = Config.Bind("Items", "enable " + item.ItemName, true, "Enable this item in game? Default: true").Value;*/
            var isEnabled = true;
            //checks to see if the config is enabled
            if (isEnabled)
            {

                //if the item is activated, instantiates the item
                item.Init(base.Config);

            }

        }

        //this method will instantiate our achievements based on a generated config option
        public void VerifyAchievements(AchievementBase achievement)
        {

            var isEnabled = Config.Bind<bool>("Items", "enable" + achievement.AchievementNameToken, true, "Enable this achievement in game? Default: true").Value;

            if (isEnabled)
            {

                achievement.Init(base.Config);

            }

        }

        //place other necessary methods below


        /*private void Update()
        {
            // This if statement checks if the player has currently pressed F2.
            if (Input.GetKeyDown(KeyCode.F2))
            {
                // Get the player body to use a position:
                var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

                // And then drop our defined item in front of the player.

                PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(myItemDef.itemIndex), transform.position, transform.forward * 20f);
            }
        }*/
    }

}
