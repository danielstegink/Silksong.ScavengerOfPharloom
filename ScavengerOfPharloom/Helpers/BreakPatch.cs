using HarmonyLib;
using ScavengerOfPharloom.Settings;
using System.Collections.Generic;
using System.Linq;
using TeamCherry.SharedUtils;
using DanielSteginkUtils.Utilities;
using UnityEngine;

namespace ScavengerOfPharloom.Helpers
{
    [HarmonyPatch(typeof(Breakable), "Break")]
    public static class BreakPatch
    {
        [HarmonyPrefix]
        public static void Prefix(Breakable __instance, float flingAngleMin, float flingAngleMax, float impactMultiplier)
        {
            //string objectPath = __instance.gameObject.name;
            //GameObject breakable = __instance.gameObject;
            //while (breakable.transform.parent != null)
            //{
            //    breakable = breakable.transform.parent.gameObject;
            //    objectPath = $"{objectPath} -> {breakable.name}";
            //}
            //ScavengerOfPharloom.Instance.Log($"Breaking {objectPath}");

            // For certain pots, structures and dead bugs, add a 50% chance of dropping a shell shard
            if (ConfigSettings.setShardDrop.Value)
            {
                List<string> shardObjects = new List<string>()
                {
                    // Weavenests
                    "loom_room_jar",

                    // Mosslands / Marrow
                    "generic_break_pot_",
                    "bone_goomba_skull_break",
                    "Bone_house_breakable_point",
                    "Bone_house_breakable_mid",
                    "Bone_house_pieces_squat",

                    // Deep Docks
                    "brk_barrel_",
                    "Dock_Table",

                    // Far Fields
                    "ant_brk_barrel_",

                    // Blasted Steps
                    "pilgrim_fossil_break",
                    "fossil_judge_break_leanRight",
                    "Hornet_Coral_Chunk_Break",

                    // Slab
                    "slab_pot_basic",
                };

                if (shardObjects.Any(x => __instance.gameObject.name.Contains(x)))
                {
                    ClassIntegrations.SetField(__instance, "shellShardDrops", new MinMaxInt(0, 1));
                }
            }

            // Certain pots in the Citadel should have a 50% chance of dropping a rosary
            if (ConfigSettings.setRosaryDrop.Value)
            {
                List<string> rosaryObjects = new List<string>()
                {
                    // Marrow
                    "rosary_shrine_break",

                    // Citadel
                    "Hornet_way_pole_harp",
                    "sc_break_pot_",
                    "sc_break_extras_detail_clutter_spike",
                    "sc_break_extras_detail_small_spike_ball",
                    "break_abacus",
                     
                    // Slab
                    "slab_pot_gold",
                };

                if (rosaryObjects.Any(x => __instance.gameObject.name.Contains(x)))
                {
                    ClassIntegrations.SetField(__instance, "smallGeoDrops", new MinMaxInt(0, 1));
                }
            }
        }

        [HarmonyPostfix]
        public static void Postfix(Breakable __instance, float flingAngleMin, float flingAngleMax, float impactMultiplier)
        {
            // Silk Fly lamps and most objects in Weavenests should have a chance of giving 1 Silk
            if (ConfigSettings.setSilkDrop.Value)
            {
                // The big lamp in the Underworks should give lots of Silk
                if (__instance.gameObject.name.Equals("lamp_pivot"))
                {
                    HeroController.instance.AddSilk(3, false);
                }
                // For some reason a ton of lamps are nested and place the breakable in an "Active" object
                else if (__instance.gameObject.name.Equals("Active"))
                {
                    if (__instance.transform.parent != null)
                    {
                        GameObject parent = __instance.transform.parent.gameObject;
                        if (parent.name.Equals("lamp") &&
                            parent.transform.parent != null)
                        {
                            GameObject parent2 = parent.transform.parent.gameObject;
                            bool zylotolLamp = parent2.name.Contains("city_lamp_") &&
                                                parent2.name.Contains("_breakable");
                            List<string> lampNames = new List<string>()
                            {
                                // Deep Docks
                                "dock_chain_lamp",

                                // Far Fields
                                "Lamp 0",
                                "ant_lamp_up_breakable",

                                // Greymoor
                                "grey_halfway_lamp_",
                                "break_grey_lamp_",

                                // Bilewater
                                "swamp_lamp_simple_hang",

                                // Slab
                                "Slab Chain Lamp",
                            };
                            if (zylotolLamp ||
                                 lampNames.Any(x => __instance.gameObject.name.Contains(x)))
                            {
                                int random = UnityEngine.Random.Range(1, 101);
                                if (random <= 50)
                                {
                                    HeroController.instance.SilkGain();
                                }
                            }
                        }
                        // Shellwood, fisherbug's room
                        else if (parent.name.Equals("cap") &&
                                    parent.transform.parent != null)
                        {
                            GameObject parent2 = parent.transform.parent.gameObject;
                            if (parent.name.Contains("shell_hang_rope"))
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
                else
                {
                    List<string> silkObjects = new List<string>()
                    {
                        // Mosslands
                        "Bone_house_pieces_post",

                        // Far Fields
                        "lamp_roof_break_always_on",

                        // Greymoor
                        "break_grey_lamp_",
                        "break_aspid_lamp",

                        // Blasted Steps
                        "break_lamp_slab_bridge",

                        // Citadel
                        "sc_mid_roof_lamp",
                        "sc_mid_room_lamp_small_breakable",
                        "break_silk_fly_jar",
                        "library_lamp_wall",
                        "Lamp_Full",
                        "library_lamp_stand",

                        // Bilewater
                        "organ lamp",

                        // Putrified Ducts
                        "organ lamp_mounted",
                        "caravan_signpost",
                    };

                    if (silkObjects.Any(x => __instance.gameObject.name.Contains(x)))
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
}