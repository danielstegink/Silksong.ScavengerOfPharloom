using BepInEx;

namespace DanielSteginkUtils;

[BepInAutoPlugin(id: "io.github.danielstegink.danielsteginkutils")]
public partial class DanielSteginkUtils : BaseUnityPlugin
{
    private void Awake()
    {
        // Put your initialization logic here
        Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");
    }
}
