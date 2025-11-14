using BepInEx.Configuration;
using TeamCherry.Localization;

namespace ScavengerOfPharloom.Settings
{
    public static class ConfigSettings
    {
        /// <summary>
        /// Integrates with UI to set shard drop setting
        /// </summary>
        public static ConfigEntry<bool> setShardDrop;

        /// <summary>
        /// Integrates with UI to set rosary drop setting
        /// </summary>
        public static ConfigEntry<bool> setRosaryDrop;

        /// <summary>
        /// Integrates with UI to set silk drop setting
        /// </summary>
        public static ConfigEntry<bool> setSilkDrop;

        /// <summary>
        /// Initializes the settings
        /// </summary>
        /// <param name="config"></param>
        public static void Initialize(ConfigFile config)
        {
            // Bind set methods to Config
            LocalisedString shardName = new LocalisedString($"Mods.{ScavengerOfPharloom.Id}", "SHARD_NAME");
            LocalisedString shardDescription = new LocalisedString($"Mods.{ScavengerOfPharloom.Id}", "SHARD_DESC");
            setShardDrop = config.Bind("Modifier", shardName, true, shardDescription);

            LocalisedString rosaryName = new LocalisedString($"Mods.{ScavengerOfPharloom.Id}", "ROSARY_NAME");
            LocalisedString rosaryDescription = new LocalisedString($"Mods.{ScavengerOfPharloom.Id}", "ROSARY_DESC");
            setRosaryDrop = config.Bind("Modifier", rosaryName, true, rosaryDescription);

            LocalisedString silkName = new LocalisedString($"Mods.{ScavengerOfPharloom.Id}", "SILK_NAME");
            LocalisedString silkDescription = new LocalisedString($"Mods.{ScavengerOfPharloom.Id}", "SILK_DESC");
            setSilkDrop = config.Bind("Modifier", silkName, true, silkDescription);
        }
    }
}