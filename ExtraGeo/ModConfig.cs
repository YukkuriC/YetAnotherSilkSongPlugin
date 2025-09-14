using BepInEx.Configuration;

namespace YetAnotherSilkSongPlugin
{
    public static class ModConfig
    {
        public const string UID = "yukkuric.silksong.mod.extra_geo";
        public const string NAME = "Extra Rosary Beads";
        public const string VERSION = "1.4.0";
        public static ConfigEntry<int> BeadsRatio;
        public static ConfigEntry<bool> IgnoreExistingBeadDrops, GachaMode;
        public static ConfigSet BEAD, SHARD;
    }

    public class ConfigSet
    {
        public readonly ConfigEntry<int> Ratio;
        public readonly ConfigEntry<bool> IgnoreExistingDrops, GachaMode;
        public ConfigSet(ConfigFile config, string category, int defaultRatio = 5)
        {
            Ratio = config.Bind(category, "Ratio", defaultRatio,
                string.Format("How many max health will be converted to 1 {0}\n(valid value must >= 1; set to <= 0 to disable)", category));
            IgnoreExistingDrops = config.Bind(category, "Ignores Existing Drops", false,
                string.Format("If set to true, those with default {0} drops will be ignored", category));
            GachaMode = config.Bind(category, "Gacha Mode", false,
                string.Format("Set to true to enable randomized increased {0}s rather than fixed value", category));
        }

        public int Get(int maxHp)
        {
            int ratio = Ratio.Value;
            if (GachaMode.Value)
            {
                int extra = 0;
                for (int i = 0; i < maxHp; i++)
                {
                    if (UnityEngine.Random.value * ratio < 1) extra++;
                }
                return extra;
            }

            return maxHp / System.Math.Max(ratio, 1);
        }
    }
}
