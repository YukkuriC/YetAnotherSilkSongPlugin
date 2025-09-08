using HarmonyLib;

namespace YetAnotherSilkSongPlugin.Patches
{
    [HarmonyPatch]
    public class PatchExtraGeo
    {
        [HarmonyPrefix, HarmonyPatch(typeof(HealthManager), "SpawnCurrency")]
        public static void ExtraGeo(ref int smallGeoCount, int ___initHp)
        {
            smallGeoCount += ___initHp / 5;
        }
    }
}
