using HarmonyLib;
using ScavengerOfPharloom.Settings;
using System.Collections.Generic;
using System.Linq;
using TeamCherry.SharedUtils;
using DanielSteginkUtils.Utilities;

namespace ScavengerOfPharloom.Helpers
{
    [HarmonyPatch(typeof(Breakable), "Break")]
    public static class BreakPatch
    {
        [HarmonyPrefix]
        public static void Prefix(Breakable __instance, float flingAngleMin, float flingAngleMax, float impactMultiplier)
        {
            //LootPots.Instance.Log($"Breaking {__instance.gameObject.name}");

            // For certain objects, such as pots, structures and skullbug masks, add a 50% chance of dropping a shell shard
            if (ConfigSettings.setShardDrop.Value)
            {
                List<string> shardObjects = new List<string>()
                {
                    "_skull_",
                    "_pot_",
                    "_cairn_",
                    "bone_house_pieces_squat",
                    "bone_house_breakable_point",
                    "bone_house_breakable_mid",
                    "barrel"
                };

                if (shardObjects.Any(x => __instance.gameObject.name.ToLower().Contains(x)))
                {
                    ClassIntegrations.SetField(__instance, "shellShardDrops", new MinMaxInt(0, 1));
                }
            }

            // Certain pots in the Citadel should have a 50% chance of dropping a rosary
            if (ConfigSettings.setRosaryDrop.Value)
            {
                List<string> rosaryObjects = new List<string>()
                {
                    "sc_break_extras_detail",
                    "sc_break_pot"
                };

                if (rosaryObjects.Any(x => __instance.gameObject.name.ToLower().Contains(x)))
                {
                    ClassIntegrations.SetField(__instance, "smallGeoDrops", new MinMaxInt(0, 1));
                }
            }
        }

        [HarmonyPostfix]
        public static void Postfix(Breakable __instance, float flingAngleMin, float flingAngleMax, float impactMultiplier)
        {
            // Silk Fly lamps should have a chance of giving 1 Silk
            if (ConfigSettings.setSilkDrop.Value)
            {
                // The big lamp in the Underworks should give lots of Silk
                if (__instance.gameObject.name.ToLower().Contains("lamp_pivot"))
                {
                    HeroController.instance.AddSilk(3, false);
                }
                else if (__instance.gameObject.name.ToLower().Contains("lamp"))
                {
                    int random = UnityEngine.Random.Range(1, 101);
                    if (random <= 50)
                    {
                        HeroController.instance.SilkGain();
                    }
                }
            }
        }
    }
}