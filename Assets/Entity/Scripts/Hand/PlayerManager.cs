using System;
using System.Collections.Generic;
using Entity.Scripts.Hand.Definitions;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Entity.Scripts.Hand
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private RoomController _RoomController;
        [SerializeField] private HandController _HandController;

        [SerializeField] private List<HandPowerUpgrade> _HandPowerUpgrade;
        [SerializeField] private List<HandSpeedUpgrade> _HandSpeedUpgrade;
        [SerializeField] private List<HandVisionUpgrade> _HandVisionUpgrade;
        
        private int _upgradeLevel = 0;

        private List<AHandUpgradeDefinition> _currentUpgrades = new List<AHandUpgradeDefinition>();

        public Signal OnInteractionKeyPressed = new Signal();

        private void Awake()
        {
            UpdateCurrentUpgrades();


            SetHand();
        }

        private void UpdateCurrentUpgrades()
        {
            _currentUpgrades.Clear();
            _currentUpgrades.Add(_HandPowerUpgrade[_upgradeLevel]);
            _currentUpgrades.Add(_HandSpeedUpgrade[_upgradeLevel]);
            _currentUpgrades.Add(_HandVisionUpgrade[_upgradeLevel]);
        }

        private void SetHand()
        {
            foreach (var upgrade in CurrentUpgrades())
            {
                if (upgrade is HandSpeedUpgrade speedUpgrade)
                {
                    _HandController.SetModifiers(speedUpgrade);
                }
            }
        }

        public void SetItemInRange(ItemTierDefinition itemTierDefinition, bool isInRange, Signs sign)
        {
            _HandController.HandSignManager.SetSign(sign, isInRange && itemTierDefinition.Tier <= _upgradeLevel);
        }
        
        public void SetSign(bool isActive, Signs sign)
        {
            _HandController.HandSignManager.SetSign(sign, isActive);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                OnInteractionKeyPressed.Dispatch();
            }
        }

        public List<AHandUpgradeDefinition> CurrentUpgrades()
        {
            var currentUpgrades = new List<AHandUpgradeDefinition>();
            foreach (var upgrade in _currentUpgrades)
            {
                currentUpgrades.Add(upgrade);
            }

            return currentUpgrades;
        }


        public void UpgradePurchased()
        {
            _upgradeLevel++;
            UpdateCurrentUpgrades();
            SetHand();
        }
    }
}