﻿using GameNetcodeStuff;
using MoreShipUpgrades.Managers;
using MoreShipUpgrades.Misc.Upgrades;
using MoreShipUpgrades.UpgradeComponents.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace MoreShipUpgrades.UpgradeComponents.OneTimeUpgrades
{
    class Sigurd : OneTimeUpgrade, IUpgradeWorldBuilding, IOneTimeUpgradeDisplayInfo
    {
        public const string UPGRADE_NAME = "Sigurd Access";
        internal const string WORLD_BUILDING_TEXT = "\n\nSigurd always laughed at Desmond when he remembered the stories about The Company paying 120% of the value of the" +
            " scrap. Before disappearing, Sigurd found a module for the ship's terminal. After installing this mysterious module, the last scrap sale that Sigurd made" +
            " was recorded at a value above normal.";
        public static Sigurd Instance;
        public static PlayerControllerB localPlayerController => StartOfRound.Instance?.localPlayerController;

        internal override void Start()
        {
            upgradeName = UPGRADE_NAME;
            base.Start();

            if (UpgradeBus.Instance.PluginConfiguration.SIGURD_ENABLED.Value && UpgradeBus.Instance.PluginConfiguration.SIGURD_PRICE.Value == 0)
            {
                LguStore.Instance.HandleUpgrade(UPGRADE_NAME, false);
            }
        }

        void Awake()
        {
            Instance = this;
        }
        public static float GetBuyingRateLastDay(float defaultValue)
        {
            if (!UpgradeBus.Instance.PluginConfiguration.SIGURD_LAST_DAY_ENABLED.Value) return defaultValue;
            if (!GetActiveUpgrade(UPGRADE_NAME)) return defaultValue;
            System.Random random = new(StartOfRound.Instance.randomMapSeed);
            if (random.Next(0, 100) < Mathf.Clamp(UpgradeBus.Instance.PluginConfiguration.SIGURD_LAST_DAY_CHANCE.Value, 0, 100))
                return defaultValue + (UpgradeBus.Instance.PluginConfiguration.SIGURD_LAST_DAY_PERCENT.Value / 100) ;
            return defaultValue;
        }

        public static float GetBuyingRate(float defaultValue)
        {
            if (!UpgradeBus.Instance.PluginConfiguration.SIGURD_ENABLED.Value) return defaultValue;
            if (!GetActiveUpgrade(UPGRADE_NAME)) return defaultValue;
            if (TimeOfDay.Instance.daysUntilDeadline == 0) return defaultValue;

            System.Random random = new(StartOfRound.Instance.randomMapSeed);
            if (random.Next(0, 100) < Mathf.Clamp(UpgradeBus.Instance.PluginConfiguration.SIGURD_CHANCE.Value, 0, 100))
                return defaultValue + (UpgradeBus.Instance.PluginConfiguration.SIGURD_PERCENT.Value / 100);
            return defaultValue;
        }

        public override string GetDisplayInfo(int price = -1)
        {
            return "There's a chance that the company will pay more.";
        }

        public string GetWorldBuildingText(bool shareStatus = false)
        {
            return WORLD_BUILDING_TEXT;
        }
    }
}