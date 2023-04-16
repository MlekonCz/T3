using Entity.Scripts.Hand;
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
        
        [SerializeField]
        private Sprite 

        private bool _isPickedUp;
        
        private Color _originalColor;

        private void Awake()
        {
            _originalColor = _ItemImage.color;
        }

        public ItemTierDefinition GetItemTierDefinition()
        {
            return _ItemTierDefinition;
        }

        public void OnPickedUp()
        {
            _ItemImage.color = _PickedUpColor;
            gameObject.GetComponent<Collider2D>().enabled = false;
            _GlowObject.SetActive(false);
        }

        public void OnBeingCaught()
        {
            _ItemImage.color = _originalColor;
            gameObject.GetComponent<Collider2D>().enabled = true;
            _GlowObject.SetActive(true);

        }

        public void OnBeingReplaced()
        {
            _ItemImage.color = new Color(1,0,1,0.8f);
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(TagManager.PLAYER))
            {
                Game.Instance.PlayerManager.SetItemInRange(_ItemTierDefinition, true, _isPickedUp ? Signs.ReplaceSign : Signs.CollectSign);
                Game.Instance.PlayerManager.OnInteractionKeyPressed.AddListener(OnInteraction);
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(TagManager.PLAYER))
            {
                Game.Instance.PlayerManager.SetItemInRange(_ItemTierDefinition, false,  _isPickedUp ? Signs.ReplaceSign : Signs.CollectSign);
                Game.Instance.PlayerManager.OnInteractionKeyPressed.RemoveListener(OnInteraction);

            }
            
        }
        
        private void OnInteraction()
        {
            var playerManager = Game.Instance.PlayerManager;
            if (playerManager.CanPickItem(_ItemTierDefinition))
            {
                playerManager.CurrentPickable = this;
                OnPickedUp();
            }
            
        }
    }
}