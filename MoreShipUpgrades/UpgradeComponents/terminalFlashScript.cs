﻿using MoreShipUpgrades.Managers;
using MoreShipUpgrades.Misc;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace MoreShipUpgrades.UpgradeComponents
{
    public class terminalFlashScript : BaseUpgrade
    {
        public static string UPGRADE_NAME = "Discombobulator";
        public static string PRICES_DEFAULT = "330,460,620";
        void Start()
        {
            upgradeName = UPGRADE_NAME;
            DontDestroyOnLoad(gameObject);
            Register();
        }

        void Update()
        {
            if (UpgradeBus.instance.flashCooldown > 0f)
            {
                UpgradeBus.instance.flashCooldown -= Time.deltaTime;
            }
        }
        public override void load()
        {
            UpgradeBus.instance.terminalFlash = true;
            UpgradeBus.instance.flashScript = this;
            
            HUDManager.Instance.chatText.text += "\n<color=#FF0000>Discombobulator is active!\nType 'cooldown' into the terminal for info!!!</color>";
        }

        public override void Register()
        {
            base.Register();
        }

        public override void Increment()
        {
            UpgradeBus.instance.discoLevel++;
        }

        public override void Unwind()
        {
            base.Unwind();

            UpgradeBus.instance.terminalFlash = false;
            UpgradeBus.instance.discoLevel = 0;
        }

        [ServerRpc(RequireOwnership = false)]
        public void PlayAudioAndUpdateCooldownServerRpc()
        {
            PlayAudioAndUpdateCooldownClientRpc();
        }

        [ClientRpc]
        private void PlayAudioAndUpdateCooldownClientRpc()
        {
            Terminal terminal = GameObject.Find("TerminalScript").GetComponent<Terminal>();
            terminal.terminalAudio.maxDistance = 100f;
            terminal.terminalAudio.PlayOneShot(UpgradeBus.instance.flashNoise);
            StartCoroutine(ResetRange(terminal));
            UpgradeBus.instance.flashCooldown = UpgradeBus.instance.cfg.DISCOMBOBULATOR_COOLDOWN;
            Collider[] array = Physics.OverlapSphere(terminal.transform.position, UpgradeBus.instance.cfg.DISCOMBOBULATOR_RADIUS, 524288);
            if(array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    EnemyAICollisionDetect component = array[i].GetComponent<EnemyAICollisionDetect>();
                    if (component != null)
                    {
                        component.mainScript.SetEnemyStunned(true, UpgradeBus.instance.cfg.DISCOMBOBULATOR_STUN_DURATION + (UpgradeBus.instance.cfg.DISCOMBOBULATOR_INCREMENT * UpgradeBus.instance.discoLevel), null);
                    }
                }
            }
        }

        private IEnumerator ResetRange(Terminal terminal)
        {
            yield return new WaitForSeconds(2f);
            terminal.terminalAudio.maxDistance = 17f;
        }

    }
}
