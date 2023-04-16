using UnityEngine;

namespace Entity.Scripts
{
    public interface IPickable
    {
        public ItemTierDefinition GetItemTierDefinition();
        
        public Sprite GetItemCopy();
        public void OnPickedUp();

        public void OnBeingCaught();
        
        public void OnBeingReplaced();

        public bool IsMissing();

    }
}