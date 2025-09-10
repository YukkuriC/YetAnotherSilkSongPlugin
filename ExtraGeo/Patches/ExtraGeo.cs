using HarmonyLib;
using UnityEngine;

namespace YetAnotherSilkSongPlugin.Patches
{
    using static ModConfig;

    [HarmonyPatch]
    public class PatchExtraGeo
    {
        [HarmonyPrefix, HarmonyPatch(typeof(HealthManager), "SpawnCurrency")]
        public static void ExtraGeo(ref int smallGeoCount, int ___initHp,
            int mediumGeoCount, int largeGeoCount, int largeSmoothGeoCount,
            bool shouldShardsFlash, bool shouldGeoFlash)
        {
            // wtf why is this called 2/3 times
            if (shouldShardsFlash || shouldGeoFlash) return;

            // check existing
            if (IgnoreExistingBeadDrops.Value && (
                smallGeoCount > 0 || mediumGeoCount > 0 || largeGeoCount > 0 || largeSmoothGeoCount > 0
            )) return;

            // calc added beads
            int ratio = BeadsRatio.Value;
            int extraBeads;
            if (GachaMode.Value)
            {
                extraBeads = 0;
                for (int i = 0; i < ___initHp; i++)
                {
                    if (Random.value * ratio < 1) extraBeads++;
                }
            }
            else
            {
                extraBeads = ___initHp / System.Math.Max(ratio, 1);
            }

            smallGeoCount += extraBeads;
        }
    }
}
