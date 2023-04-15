namespace Entity.Scripts
{
    public interface IPickable
    {
        public ItemTierDefinition GetItemTierDefinition();
        public void OnPickedUp();

        public void OnBeingCaught();
        
        public void OnBeingReplaced();

    }
}