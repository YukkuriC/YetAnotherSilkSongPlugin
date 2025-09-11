using GlobalEnums;
using HarmonyLib;
using UnityEngine;
using System.Reflection;
using System.Collections;

namespace YetAnotherSilkSongPlugin.Patches
{
    [HarmonyPatch]
    public class PatchExtraGeo
    {
        const BindingFlags flagPrivateMethod = BindingFlags.NonPublic | BindingFlags.Instance;
        static MethodInfo methodHeroDamaged = typeof(HeroController).GetMethod("HeroDamaged", flagPrivateMethod);
        static MethodInfo methodDieFromHazard = typeof(HeroController).GetMethod("DieFromHazard", flagPrivateMethod);
        static object[] paramHeroDamaged = new object[0];
        static object[] paramDieFromHazard = new object[2] { null, 0 };

        [HarmonyPrefix, HarmonyPatch(typeof(HeroController), nameof(HeroController.TakeDamage))]
        static bool CheckHazardDamage(GameObject go, HazardType hazardType, HeroController __instance)
        {
            if (hazardType <= HazardType.ENEMY) return true;
            // partial hurt effect
            methodHeroDamaged.Invoke(__instance, paramHeroDamaged);
            __instance.CancelAttack();
            __instance.RegainControl();
            __instance.StartAnimationControl();
            // do respawn
            if (hazardType == HazardType.RESPAWN_PIT) hazardType = HazardType.PIT;
            paramDieFromHazard[0] = hazardType;
            paramDieFromHazard[1] = (hazardType == HazardType.SPIKES || hazardType == HazardType.COAL_SPIKES) ? (go?.transform.rotation.z ?? 0) : 0;
            __instance.StartCoroutine((IEnumerator)methodDieFromHazard.Invoke(__instance, paramDieFromHazard));
            return false;
        }
    }
}
