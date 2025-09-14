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
            ref int mediumGeoCount, int largeGeoCount, int largeSmoothGeoCount, ref int shellShardCount,
            bool shouldShardsFlash, bool shouldGeoFlash)
        {
            // wtf why is this called 2/3 times
            if (shouldShardsFlash || shouldGeoFlash) return;
            int maxHp = MaxHealthTracker.Get(__instance, ___initHp);

            // extra shards
            bool hasBase = shellShardCount > 0;
            if ((!hasBase) || SHARD.IgnoreExistingDrops.Value)
            {
                shellShardCount += SHARD.Get(maxHp);
            }

            // extra beads
            hasBase = smallGeoCount > 0 || mediumGeoCount > 0 || largeGeoCount > 0 || largeSmoothGeoCount > 0;
            if ((!hasBase) || BEAD.IgnoreExistingDrops.Value)
            {
                smallGeoCount += BEAD.Get(maxHp);
                // merge beads to larger
                int mergedBeads = smallGeoCount / 5;
                mediumGeoCount += mergedBeads;
                smallGeoCount -= mergedBeads * 5;
            }
        }

        [HarmonyPrefix, HarmonyPatch(typeof(HealthManager), "TakeDamage")]
        public static void RecordHPBeforeHit(HealthManager __instance)
        {
            MaxHealthTracker.Track(__instance);
        }
    }
}
