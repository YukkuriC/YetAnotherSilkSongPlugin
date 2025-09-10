using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace YetAnotherSilkSongPlugin
{
    using static ModConfig;

    [BepInPlugin(UID, NAME, VERSION)]
    [BepInProcess("Hollow Knight Silksong.exe")]
    public class Entry_BepInEx : BaseUnityPlugin
    {
        static Harmony patcher;

        void Awake()
        {
            patcher = new Harmony(UID);
            patcher.PatchAll();

            // configs
            BeadsRatio = Config.Bind("General", "Beads Ratio", 5, "How many max health will be converted to 1 bead (must >= 1)");
            IgnoreExistingBeadDrops = Config.Bind("General", "Ignores Existing Bead Drops", false, "If set to true, those with default bead drops will be ignored");
            GachaMode = Config.Bind("General", "Gacha Mode", false, "Set to true to enable randomized increased beads rather than fixed value");
        }
    }
}
