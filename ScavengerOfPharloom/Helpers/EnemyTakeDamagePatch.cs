using HarmonyLib;
using ScavengerOfPharloom.Settings;

namespace ScavengerOfPharloom.Helpers
{
    [HarmonyPatch(typeof(HealthManager), "TakeDamage")]
    public static class EnemyTakeDamagePatch
    {
        [HarmonyPostfix]
        public static void Postfix(HealthManager __instance, HitInstance hitInstance)
        {
            // Cogwork constructs release Silk Flies when destroyed, so they should also give silk when destroyed
            if (ConfigSettings.setSilkDrop.Value)
            {
                if (__instance.isDead &&
                    __instance.gameObject.name.ToLower().Contains("automaton"))
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
