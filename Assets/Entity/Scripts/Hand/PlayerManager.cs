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
            _currentUpgrades.Add(_HandPowerUpgrade[_upgradeLevel]);
            _currentUpgrades.Add(_HandSpeedUpgrade[_upgradeLevel]);
            _currentUpgrades.Add(_HandVisionUpgrade[_upgradeLevel]);
            
            
            
            SetHand();
        }

        private void SetHand()
        {
            foreach (var upgrade in CurrentUpgrades())
            {
                if (upgrade is HandSpeedUpgrade speedUpgrade)
                {
                    _HandController.Initialize(speedUpgrade);
                }
            }
        }

        public void ItemInRange(ItemTierDefinition itemTierDefinition, bool isInRange, Signs sign)
        {
            _HandController.HandSignManager.SetSign(sign, isInRange && itemTierDefinition.Tier <= _upgradeLevel);
          
        }
        
        
        private void Update()
        {
            
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
        
        

    }
}