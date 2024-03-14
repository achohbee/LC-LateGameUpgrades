﻿using MoreShipUpgrades.Managers;
using MoreShipUpgrades.Misc.Upgrades;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoreShipUpgrades.UpgradeComponents.OneTimeUpgrades
{
    class FasterDropPod : OneTimeUpgrade
    {
        public const string UPGRADE_NAME = "Drop Pod Thrusters";
        public static FasterDropPod Instance;

        internal override void Start()
        {
            upgradeName = UPGRADE_NAME;
            base.Start();

            if (UpgradeBus.Instance.PluginConfiguration.FASTER_DROP_POD_ENABLED.Value && UpgradeBus.Instance.PluginConfiguration.FASTER_DROP_POD_PRICE.Value == 0)
            {
                LguStore.Instance.HandleUpgrade(UPGRADE_NAME, false);
            }
        }

        void Awake()
        {
            Instance = this;
        }
        public static float GetFirstOrderTimer(float defaultValue)
        {
            if (!UpgradeBus.Instance.PluginConfiguration.FASTER_DROP_POD_ENABLED.Value) return defaultValue;
            if (!GetActiveUpgrade(UPGRADE_NAME)) return defaultValue;
            return Mathf.Clamp(defaultValue + UpgradeBus.Instance.PluginConfiguration.FASTER_DROP_POD_INITIAL_TIMER.Value, defaultValue, defaultValue + UpgradeBus.Instance.PluginConfiguration.FASTER_DROP_POD_TIMER.Value);
        }
        public static float GetUpgradedTimer(float defaultValue)
        {
            if (!UpgradeBus.Instance.PluginConfiguration.FASTER_DROP_POD_ENABLED.Value) return defaultValue;
            if (!GetActiveUpgrade(UPGRADE_NAME)) return defaultValue;
            return Mathf.Clamp(defaultValue - UpgradeBus.Instance.PluginConfiguration.FASTER_DROP_POD_TIMER.Value, 0f, defaultValue);
        }

        public override string GetDisplayInfo(int price = -1)
        {
            return "Make the Drop Pod, the ship that deliver items bought on the terminal, land faster.";
        }
    }
}