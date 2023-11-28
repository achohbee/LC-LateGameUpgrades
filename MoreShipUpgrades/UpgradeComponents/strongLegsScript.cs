﻿using GameNetcodeStuff;
using MoreShipUpgrades.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace MoreShipUpgrades.UpgradeComponents
{
    internal class strongLegsScript : NetworkBehaviour
    {
        void Start()
        {
            StartCoroutine(lateApply());
        }

        private IEnumerator lateApply()
        {
            yield return new WaitForSeconds(1);
            UpgradeBus.instance.strongLegs = true;
            PlayerControllerB[] players = GameObject.FindObjectsOfType<PlayerControllerB>();
            foreach (PlayerControllerB player in players)
            {
                player.jumpForce = Plugin.cfg.JUMP_FORCE;
            }
            transform.parent = GameObject.Find("HangarShip").transform;
            HUDManager.Instance.chatText.text += "\n<color=#FF0000>Strong Legs is active!</color>";
        }
    }
}
