using System;
using Entity.Scripts.Hand;
using Entity.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Entity.Scripts
{
    public class Item : MonoBehaviour, IPickable
    {
        [SerializeField] private ItemTierDefinition _ItemTierDefinition;

        [SerializeField] private SpriteRenderer _ItemImage;

        [SerializeField] private Color _PickedUpColor;

        [SerializeField] private GameObject _CollectSign;
        
        private Color _originalColor;

        private void Awake()
        {
            _originalColor = _ItemImage.color;
            _CollectSign.SetActive(false);

        }

        public ItemTierDefinition GetItemTierDefinition()
        {
            return _ItemTierDefinition;
        }

        public void OnPickedUp()
        {
            _ItemImage.color = _PickedUpColor;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

        public void OnBeingCaught()
        {
            _ItemImage.color = _originalColor;
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        public void OnBeingReplaced()
        {
            _ItemImage.color = new Color(1,0,1,0.8f);
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(StringManager.PLAYER))
            {
                _CollectSign.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(StringManager.PLAYER))
            {
                _CollectSign.SetActive(false);
            }
            
        }
    }
}