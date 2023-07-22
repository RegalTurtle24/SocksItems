using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2GenericModTemplate1.Base_Classes;
using System;
using System.Reflection;
using UnityEngine;

using RoR2;
using SocksNeedsItems;

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
            // whites
            // Six Sided Die [10]
            VerifyItems(new Items.Dragon_Horn());
            VerifyItems(new Items.Omnis_Rend());
            // Old Notebook [7]
            // Bottle of Mud [4]
            // Holographic Ad [7]

            // greens
            // Bronze Vajra [8]
            // Era Protect [4]
            // Kjaro and Runald's Vows [8]
            // Edge of the Storm [5]
            // Pipe Bomb [8]
            // Glowing Spores [6]
            // Scarab Skull [6]
            // Quantum Processing Unit [2]
            // Quantum Entanglers [4]
            // Mobius Loop [8]

            // red
            // Executioner's Blade [7]
            // Adorable Socks [10]
            // Legend Calendar [4]
            // Forgive the World [7]
            // Scars of the Broken one [2]
            // Regalia of Dawn [5]
            // Fissile Fuel Rod [5]
            // Shattered Void Cube [8]
            // Shrodinger's Cat [1]
            // Rocket Propelled Chainsaw [5]
            
            // void white
            // Twenty-One Sided Die [2] (Six-Sided Die)
            // Ancient Heiroglyphs [1] (Old Notebook)
            // Twisted Tincture [2] (Alternate Reality Visor; other mod)
            // Swirling Slurry [3]

            // void green
            // Death's Touch [4] (Bronze Vajras)
            // A Sock's Luck [4] (Defibrillator; other mod)
            // Twisted Cordyceps [6] (Glowing Spores)
            // Jailer's Right Hand [7] (Prison Shackles; other mod)
            // Antimatter Pipette [4] (LETH-42; other mod)
            // Negative Energy Generator [4] (Quantum Entanglers)
            // Unheard Canticles [6]

            // void reds
            // Parasitic Tarantula [3] (Symbiotic Scorpion; base game)
            // Antimatter Capsule [4] (Fissile Fuel Rods)
            // Wretched Opal [2] (Ancient Scepter; other mod)
            // Imitation [8]
            
            // Equipment
            // Captain's Hotline [4]
            // The Crowd [4]
            // Elongated Knuckle Dusters [3]
            // Balisong [5]
            // Distress Beacon [2]
            // Orbital Medkit [2]
            // Pocket White Hole [3]
            // Warp Drive [5]
            
            // Boss Item
            // Hammer of Design [5] (Mithrix)
            
            // Lunars
            // Moon Pie [2]
            // Starve the Unforgotten [1]
            // Corrupt Man's Credit Card [3]
            // Strange Matter [5]
            // Timerly Demise [3]

            // Lunar Equip
            // Entropy Key [2]



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

    }

}
