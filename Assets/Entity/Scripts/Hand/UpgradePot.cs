using System;
using Entity.Scripts.Utilities;
using UnityEngine;

namespace Entity.Scripts.Hand
{
    public class UpgradePot : MonoBehaviour
    {
        
        
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("Hand entered the room");
            if (!col.gameObject.CompareTag(TagManager.PLAYER)) return;

            var instance = Game.Instance;
            instance.PlayerManager.SetSign(instance.ScoreManager.LifetimeScore > 0, Signs.UpgradeSign);
                
            instance.PlayerManager.OnInteractionKeyPressed.AddListener(OnInteraction);
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(TagManager.PLAYER)) return;

            Game.Instance.PlayerManager.SetSign(false, Signs.UpgradeSign);
                
            Game.Instance.PlayerManager.OnInteractionKeyPressed.RemoveListener(OnInteraction);
            
        }

        private void OnInteraction()
        {
            var instance = Game.Instance;

            var upgradeCost = instance.PlayerManager.CurrentUpgrades()[0].Cost;
            if (!instance.ScoreManager.HasEnoughScore(upgradeCost)) return;

            
            instance.PlayerManager.OnInteractionKeyPressed.RemoveListener(OnInteraction);
            instance.PlayerManager.UpgradePurchased();
            instance.ScoreManager.SubtractScore(upgradeCost);
        }
    }
}