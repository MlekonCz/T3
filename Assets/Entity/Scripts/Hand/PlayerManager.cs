using System;
using System.Collections.Generic;
using Entity.Scripts.Hand.Definitions;
using UnityEngine;

namespace Entity.Scripts.Hand
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private RoomController _RoomController;

        [SerializeField] private List<HandPowerUpgrade> _HandPowerUpgrade;
        [SerializeField] private List<HandSpeedUpgrade> _HandSpeedUpgrade;
        [SerializeField] private List<HandVisionUpgrade> _HandVisionUpgrade;
        
        private int _upgradeLevel = 0;

        private List<AHandUpgradeDefinition> _currentUpgrades = new List<AHandUpgradeDefinition>();


        private void Awake()
        {
            _currentUpgrades.Add(_HandPowerUpgrade[_upgradeLevel]);
            _currentUpgrades.Add(_HandSpeedUpgrade[_upgradeLevel]);
            _currentUpgrades.Add(_HandVisionUpgrade[_upgradeLevel]);
            
            _RoomController.Initialize(CurrentUpgrades());

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