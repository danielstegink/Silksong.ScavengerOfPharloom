using HarmonyLib;
using ScavengerOfPharloom.Settings;
namespace ScavengerOfPharloom.Helpers
{
    [HarmonyPatch(typeof(LifebloodPustule), "Break")]
    public static class LifebloodBreakPatch
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            // Lifeblood blobs in Wormways should have a 50% chance of giving lifeblood
            if (ConfigSettings.setLifebloodDrop.Value)
            {
                int random = UnityEngine.Random.Range(1, 101);
                if (random <= 50)
                {
                    EventRegister.SendEvent(EventRegisterEvents.AddBlueHealth);
                }
            }
        }
    }
}