using System;
using System.Collections.Generic;
using Entity.Scripts.Hand.Definitions;
using Entity.Scripts.Utilities;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Entity.Scripts.Hand
{
    public class RoomController : MonoBehaviour
    {
        [SerializeField] private HandController _HandController;

        public Signal<float> OnPickableConsumed;

        private void Awake()
        {
        }

        private void Update()
        {
        
        }

       
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("Hand entered the room");
            if (col.gameObject != _HandController.gameObject) return;

            Game.Instance.PlayerManager.SetSign(_HandController.Pickable != null, Signs.FeedSign);
                
            Game.Instance.PlayerManager.OnInteractionKeyPressed.AddListener(OnInteraction);
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(StringManager.PLAYER)) return;

            Game.Instance.PlayerManager.SetSign(false, Signs.FeedSign);
                
            Game.Instance.PlayerManager.OnInteractionKeyPressed.RemoveListener(OnInteraction);
            
        }

        private void OnInteraction()
        {
            if (_HandController.Pickable == null) return;
            Game.Instance.ScoreManager.AddScore((int)_HandController.Pickable.GetItemTierDefinition().Reward);
            Game.Instance.PlayerManager.OnInteractionKeyPressed.RemoveListener(OnInteraction);
        }
    }
}
