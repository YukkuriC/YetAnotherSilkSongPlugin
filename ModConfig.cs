using BepInEx.Configuration;

namespace YetAnotherSilkSongPlugin
{
    public static class ModConfig
    {
        public const string UID = "yukkuric.silksong.mod.extra_geo";
        public const string NAME = "Extra Rosary Beads";
        public const string VERSION = "1.1.0";
        public static ConfigEntry<int> BeadsRatio;
        public static ConfigEntry<bool> IgnoreExistingBeadDrops, GachaMode;
    }
}
