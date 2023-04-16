using System;
using System.Collections.Generic;
using DG.Tweening;
using Entity.Scripts.Hand.Definitions;
using Entity.Scripts.Items;
using Entity.Scripts.Utilities;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Entity.Scripts.Hand
{
    public class RoomController : MonoBehaviour
    {
        [SerializeField] private HandController _HandController;

        [SerializeField] private Transform _SpawnPosition;

        [SerializeField] private FakeItem _FakeItem;

        private IPickable _craftedItem;
        public Signal<float> OnPickableConsumed;

      
        private void CraftFakeItem(IPickable playerManagerCurrentPickable)
        {
            if (playerManagerCurrentPickable.GetItemTierDefinition().Tier is not (2 or 3)) return;
            _craftedItem = playerManagerCurrentPickable;
            Invoke(nameof(BuildItem), playerManagerCurrentPickable.GetItemTierDefinition().CopyCreationTime);
        }

        private void BuildItem()
        {
            var item = Instantiate(_FakeItem, _SpawnPosition);
            item.transform.localPosition = Vector3.zero;
            item.SetFakeItem(_craftedItem.GetItemCopy(), _craftedItem);
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
            CraftFakeItem(Game.Instance.PlayerManager.CurrentPickable);
            Game.Instance.ScoreManager.AddScore((int)Game.Instance.PlayerManager.CurrentPickable.GetItemTierDefinition().Reward);
            Game.Instance.PlayerManager.OnInteractionKeyPressed.RemoveListener(OnInteraction);

            Game.Instance.PlayerManager.CurrentPickable = null;
            Game.Instance.PlayerManager.SetSign(false, Signs.FeedSign);

        }
    }
}
