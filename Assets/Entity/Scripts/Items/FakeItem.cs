using Entity.Scripts.Hand;
using Entity.Scripts.Utilities;
using UnityEngine;

namespace Entity.Scripts.Items
{
    public class FakeItem : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer _SpriteRenderer;


        private IPickable _pickable;


        public void SetFakeItem(Sprite sprite, IPickable pickable)
        {
            _SpriteRenderer.sprite = sprite;
            _pickable = pickable;
        }
        
        
        
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(TagManager.PLAYER))
            {
                Game.Instance.PlayerManager.SetSign(true, Signs.PickUpSign);
                Game.Instance.PlayerManager.OnInteractionKeyPressed.AddListener(OnInteraction);
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(TagManager.PLAYER))
            {
                Game.Instance.PlayerManager.SetSign(false, Signs.PickUpSign);
                Game.Instance.PlayerManager.OnInteractionKeyPressed.RemoveListener(OnInteraction);
            }
            
        }
        
        private void OnInteraction()
        {
            var playerManager = Game.Instance.PlayerManager;
            if (playerManager.CanPickItem(_pickable.GetItemTierDefinition()))
            {
                playerManager.CurrentFakeItem = _pickable;
                Destroy(gameObject);
            }
            
        }
    }
}