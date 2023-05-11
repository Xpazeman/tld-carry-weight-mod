using HarmonyLib;
using Il2Cpp;
using UnityEngine;
using MelonLoader;

namespace CarryWeightMod
{
    [HarmonyPatch(typeof(Encumber), nameof(Encumber.Start))]
    internal class Encumber_Start
    {
        private static void Postfix(Encumber __instance)
        {
            CarryWeightMod.EncumberUpdate(__instance);
        }
    }

    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.CalculateModifiedCalorieBurnRate), new Type[] { typeof(float) })]
    internal class PlayerManager_CalculateModifiedCalorieBurnRate
    {
        private static void Postfix(PlayerManager __instance, float baseBurnRate, ref float __result)
        {
            float rate = baseBurnRate;

            if (__instance.PlayerIsSprinting() || __instance.PlayerIsWalking() || __instance.PlayerIsClimbing())
            {
                rate += (GameManager.GetEncumberComponent().GetHourlyCalorieBurnFromWeight() * (30f / GameManager.GetEncumberComponent().m_MaxCarryCapacityKG));
            }
            if (GameManager.GetFreezingComponent().IsFreezing())
            {
                rate *= GameManager.GetFreezingComponent().m_CalorieBurnMultiplier;
            }
            if (__instance.PlayerIsSprinting() || __instance.PlayerIsWalking())
            {
                float y = GameManager.GetVpFPSPlayer().Controller.Velocity.normalized.y;
                if (y > 0.1f)
                {
                    float speed = (y - 0.1f) / 0.5f;
                    speed = Mathf.Clamp(speed, 0f, 1f);
                    float speedMod = Mathf.Lerp(1f, 1.5f, speed);
                    rate *= speedMod;
                }
            }
            rate *= GameManager.GetExperienceModeManagerComponent().GetCalorieBurnScale();
            float moddedBurn = rate * GameManager.GetFeatEfficientMachine().ReduceCaloriesScale();

            __result = moddedBurn;
        }
    }
}
