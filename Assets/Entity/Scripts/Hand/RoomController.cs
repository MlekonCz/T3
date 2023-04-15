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
            if (!col.gameObject.CompareTag(TagManager.PLAYER)) return;

            Game.Instance.PlayerManager.SetSign(Game.Instance.PlayerManager.CurrentPickable != null, Signs.FeedSign);
                
            Game.Instance.PlayerManager.OnInteractionKeyPressed.AddListener(OnInteraction);
            
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag(TagManager.PLAYER)) return;

            Game.Instance.PlayerManager.SetSign(false, Signs.FeedSign);
                
            Game.Instance.PlayerManager.OnInteractionKeyPressed.RemoveListener(OnInteraction);
            
        }

        private void OnInteraction()
        {
            if (Game.Instance.PlayerManager.CurrentPickable == null) return;
            Game.Instance.ScoreManager.AddScore((int)Game.Instance.PlayerManager.CurrentPickable.GetItemTierDefinition().Reward);
            Game.Instance.PlayerManager.OnInteractionKeyPressed.RemoveListener(OnInteraction);
        }
    }
}
