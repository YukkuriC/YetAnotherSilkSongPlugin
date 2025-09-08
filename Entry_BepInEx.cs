using BepInEx;
using HarmonyLib;

namespace YetAnotherSilkSongPlugin
{
    [BepInPlugin(ModConfig.UID, ModConfig.NAME, ModConfig.VERSION)]
    [BepInProcess("Hollow Knight Silksong.exe")]
    public class Entry_BepInEx : BaseUnityPlugin
    {
        static Harmony patcher;
        void Awake()
        {
            patcher = new Harmony(ModConfig.UID);
            patcher.PatchAll();
        }
    }
}
