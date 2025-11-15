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
            if (shardName.Exists &&
                shardDescription.Exists)
            {
                setShardDrop = config.Bind("Modifier", shardName, true, shardDescription);
            }
            else
            {
                setShardDrop = config.Bind("Modifier", "Shell Pots", true, "Allows pots, statues and more to drop shell shards");
            }

            LocalisedString rosaryName = new LocalisedString($"Mods.{ScavengerOfPharloom.Id}", "ROSARY_NAME");
            LocalisedString rosaryDescription = new LocalisedString($"Mods.{ScavengerOfPharloom.Id}", "ROSARY_DESC");
            if (rosaryName.Exists &&
                rosaryDescription.Exists)
            {
                setRosaryDrop = config.Bind("Modifier", rosaryName, true, rosaryDescription);
            }
            else
            {
                setRosaryDrop = config.Bind("Modifier", "Wealth of the Citadel", true, "Allows pots in the Citadel to drop rosaries");
            }

            LocalisedString silkName = new LocalisedString($"Mods.{ScavengerOfPharloom.Id}", "SILK_NAME");
            LocalisedString silkDescription = new LocalisedString($"Mods.{ScavengerOfPharloom.Id}", "SILK_DESC");
            if (silkName.Exists &&
                silkDescription.Exists)
            {
                setSilkDrop = config.Bind("Modifier", silkName, true, silkDescription);
            }
            else
            {
                setSilkDrop = config.Bind("Modifier", "Gratitude of the Silk Flies", true, "Allows Silk Flies to replenish Silk when freed");
            }
        }
    }
}