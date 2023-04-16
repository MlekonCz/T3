using System;
using DG.Tweening;
using Entity.Scripts.Items;
using Entity.Scripts.Utilities;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.Scripts.Hand
{
    public class RoomController : MonoBehaviour
    {
        [SerializeField] private HandController _HandController;

        [SerializeField] private Transform _SpawnPosition;

        [SerializeField] private FakeItem _FakeItem;

        [SerializeField] private Image _SliderImage;

        private IPickable _craftedItem;
        public Signal<float> OnPickableConsumed;


        private void Start()
        {
            _SliderImage.transform.localScale = new Vector3(0, 1, 1);
        }

        private void CraftFakeItem(IPickable playerManagerCurrentPickable)
        {
            if (playerManagerCurrentPickable.GetItemTierDefinition().Tier is not (2 or 3)) return;
            _craftedItem = playerManagerCurrentPickable;
            _SliderImage.transform.parent.gameObject.SetActive(true);
            
            _SliderImage.transform.localScale = new Vector3(0, 1, 1);
            _SliderImage.transform.DOScaleX(1, playerManagerCurrentPickable.GetItemTierDefinition().CopyCreationTime);
            Invoke(nameof(BuildItem), playerManagerCurrentPickable.GetItemTierDefinition().CopyCreationTime);
        }

        private void BuildItem()
        {
            var item = Instantiate(_FakeItem, _SpawnPosition);
            item.transform.localPosition = Vector3.zero;
            item.SetFakeItem(_craftedItem.GetItemCopy(), _craftedItem);
            _SliderImage.transform.localScale = new Vector3(1, 1, 1);
            _SliderImage.transform.parent.gameObject.SetActive(false);

        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("Hand entered the room");
            if (!col.gameObject.CompareTag(TagManager.PLAYER)) return;

            Game.Instance.PlayerManager.SetSign(Game.Instance.PlayerManager.CurrentPickable != null, Signs.FeedSign);
            Game.Instance.PlayerManager.SetSign(false, Signs.FeedTheRoomSign);

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
            Game.Instance.PlayerManager.SetSign(false,Signs.FeedSign);

            Game.Instance.PlayerManager.CurrentPickable = null;

        }
    }
}
