using BepInEx;
using HarmonyLib;
using ScavengerOfPharloom.Settings;

namespace ScavengerOfPharloom;

[BepInAutoPlugin(id: "io.github.danielstegink.scavengerofpharloom")]
[BepInDependency("org.silksong-modding.i18n")]
public partial class ScavengerOfPharloom : BaseUnityPlugin
{
    private void Awake()
    {
        // Put your initialization logic here
        Harmony harmony = new Harmony(Id);
        harmony.PatchAll();

        Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");
    }

    private void Start()
    {
        ConfigSettings.Initialize(Config);
    }
}