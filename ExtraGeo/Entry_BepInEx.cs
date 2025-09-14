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
            BEAD = new ConfigSet(Config, "Bead");
            SHARD = new ConfigSet(Config, "Shard");
        }
    }
}
