﻿using HarmonyLib;
using MoreShipUpgrades.Managers;
using MoreShipUpgrades.Misc;
using MoreShipUpgrades.UpgradeComponents.OneTimeUpgrades;
using UnityEngine;


namespace MoreShipUpgrades.Patches.Weather
{
    [HarmonyPatch(typeof(StormyWeather))]
    internal class StormyWeatherPatcher
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(StormyWeather.LightningStrike))]
        static void CheckIfLightningRodPresent(StormyWeather __instance, ref Vector3 strikePosition, bool useTargetedObject)
        {
            if (LightningRod.instance != null && LightningRod.instance.LightningIntercepted && useTargetedObject)
            {
                // we need to check useTargetedObject so we're not rerouting random strikes to the ship.
                LightningRod.RerouteLightningBolt(ref strikePosition, ref __instance);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        static void InterceptSelectedObject(StormyWeather __instance, GrabbableObject ___targetingMetalObject)
        {
            if (!UpgradeBus.instance.lightningRod || !LGUStore.instance.IsHost || !LGUStore.instance.IsServer) { return; }
            if (___targetingMetalObject == null)
            {
                if (LightningRod.instance != null) // Lightning rod could be disabled so we wouldn't have an instance
                    LightningRod.instance.CanTryInterceptLightning = true;
            }
            else
            {
                LightningRod.TryInterceptLightning(ref __instance, ref ___targetingMetalObject);
            }
        }
    }
}
