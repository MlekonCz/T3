using System;
using System.Collections.Generic;
using Entity.Scripts.Hand;
using Entity.Scripts.Hand.Definitions;
using Entity.Scripts.Utilities;
using UnityEngine;

namespace Entity.Scripts.Items
{
    public class Item : MonoBehaviour, IPickable
    {
        [SerializeField] private ItemTierDefinition _ItemTierDefinition;

        [SerializeField] private SpriteRenderer _ItemImage;

        [SerializeField] private Color _PickedUpColor;

        [SerializeField] private GameObject _GlowObject;

        [SerializeField] private Sprite _ReplacementSprite;

        private bool _isPickedUp;

        private bool _isMissing = false;
        
        private Color _originalColor;

        private void Awake()
        {
            _originalColor = _ItemImage.color;
            Game.Instance.PlayerManager.SignalOnPlayerUpgraded.AddListener(OnUpgradeCompleted);

        }

        private void Start()
        {
            if (_ItemTierDefinition.Tier > 1)
            {
                
            }
            _GlowObject.SetActive(_ItemTierDefinition.Tier <= Game.Instance.PlayerManager.CurrentUpgrades()[0].Tier && !_isPickedUp);
        }

        private void OnDestroy()
        {
            Game.Instance.PlayerManager.SignalOnPlayerUpgraded.RemoveListener(OnUpgradeCompleted);

        }

        private void OnUpgradeCompleted(List<AHandUpgradeDefinition> obj)
        {
            _GlowObject.SetActive(_ItemTierDefinition.Tier <= obj[0].Tier && !_isPickedUp);
        }

        public ItemTierDefinition GetItemTierDefinition()
        {
            return _ItemTierDefinition;
        }

        public Sprite GetItemCopy()
        {
            return _ReplacementSprite;
        }

        public void OnPickedUp()
        {
            _isPickedUp = true;
            _isMissing = true;
            _ItemImage.color = _PickedUpColor;
           // gameObject.GetComponent<Collider2D>().enabled = false;
           Game.Instance.PlayerManager.SetSign(false,Signs.CollectSign);

            _GlowObject.SetActive(false);

            if (_ItemTierDefinition.Tier == 1 || _ItemTierDefinition.Tier == 4)
            {
                gameObject.SetActive(false);
            }
        }

        public void OnBeingCaught()
        {
            _ItemImage.color = _originalColor;
            gameObject.GetComponent<Collider2D>().enabled = true;
            _GlowObject.SetActive(true);

        }

        public void OnBeingReplaced()
        {
            _isMissing = false;
            if (_ReplacementSprite)
            {
                _ItemImage.color = new Color(1,1,1,0.8f);
                _ItemImage.sprite = _ReplacementSprite;
            }
            else
            {
                _ItemImage.color = new Color(1,0,1,0.8f);
            }
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

        public bool IsMissing()
        {
            return _isMissing;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(TagManager.PLAYER))
            {
                var playerManager = Game.Instance.PlayerManager;
                if (ReferenceEquals(playerManager.CurrentFakeItem, this))
                {
                    playerManager.SetSign(true,Signs.ReplaceSign);
                    playerManager.OnInteractionKeyPressed.AddListener(OnInteraction);
                }
                else if (playerManager.CanPickItem(_ItemTierDefinition))
                {
                    playerManager.SetSign(true,Signs.CollectSign);
                    playerManager.OnInteractionKeyPressed.AddListener(OnInteraction);
                }
              
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(TagManager.PLAYER))
            {
                var playerManager = Game.Instance.PlayerManager;
                playerManager.SetSign(false,Signs.ReplaceSign);
                playerManager.SetSign(false,Signs.CollectSign);
                playerManager.OnInteractionKeyPressed.RemoveListener(OnInteraction);
            }
            
        }
        
        private void OnInteraction()
        {
            var playerManager = Game.Instance.PlayerManager;
            if (ReferenceEquals(playerManager.CurrentFakeItem, this))
            {
               OnBeingReplaced();
               playerManager.CurrentFakeItem = null;
            }
            else if (playerManager.CanPickItem(_ItemTierDefinition))
            {
                playerManager.SetSign(true,Signs.FeedSign);
                playerManager.CurrentPickable = this;
                OnPickedUp();
            }
            
            
        }
    }
}