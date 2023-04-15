using System;
using System.Collections.Generic;
using Entity.Scripts.Hand.Definitions;
using UnityEngine;

namespace Entity.Scripts.Hand
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private RoomController _RoomController;

        [SerializeField] private HandPowerUpgrade _HandPowerUpgrade;
        [SerializeField] private HandSpeedUpgrade _HandSpeedUpgrade;
        [SerializeField] private HandVisionUpgrade _HandVisionUpgrade;


        //private List<AHandUpgradeDefinition> _currentUpgrades = new List<AHandUpgradeDefinition>();

        private Dictionary<AHandUpgradeDefinition, int> _leverPerUpgrade = new Dictionary<AHandUpgradeDefinition, int>();


        private void Awake()
        {
            _RoomController.Initialize(CurrentUpgrades());
        }

        public List<AHandUpgradeDefinition> CurrentUpgrades()
        {
            var currentUpgrades = new List<AHandUpgradeDefinition>();
            foreach (var kvp in _leverPerUpgrade)
            {
                currentUpgrades.Add(kvp.Key);
            }

            return currentUpgrades;
        }
        
        

    }
}