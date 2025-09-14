using HarmonyLib;
using UnityEngine;

namespace YetAnotherSilkSongPlugin.Patches
{
    using static ModConfig;

    [HarmonyPatch]
    public class PatchExtraGeo
    {
        [HarmonyPrefix, HarmonyPatch(typeof(HealthManager), "SpawnCurrency")]
        public static void ExtraGeo(ref int smallGeoCount, int ___initHp, HealthManager __instance,
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
            int maxHp = MaxHealthTracker.Get(__instance, ___initHp);
            int ratio = BeadsRatio.Value;
            int extraBeads;
            if (GachaMode.Value)
            {
                extraBeads = 0;
                for (int i = 0; i < maxHp; i++)
                {
                    if (Random.value * ratio < 1) extraBeads++;
                }
            }
            else
            {
                extraBeads = maxHp / System.Math.Max(ratio, 1);
            }

            smallGeoCount += extraBeads;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(HealthManager), "TakeDamage")]
        public static void RecordHPBeforeHit(HealthManager __instance)
        {
            MaxHealthTracker.Track(__instance);
        }
    }
}
