using UnityEngine;

namespace Entity.Scripts
{
    [CreateAssetMenu(fileName = "ItemTier", menuName = "ItemTier", order = 0)]
    public class ItemTierDefinition : ScriptableObject
    {
        [SerializeField]
        public float _Reward;
        public float Reward => _Reward;
        
        [SerializeField]
        public float _SlowModifier;
        public float SlowModifier => _SlowModifier;
        
        [SerializeField]
        public float _PickUpTime;
        public float PickUpTime => _PickUpTime;
        
        [SerializeField]
        public float _CopyCreationTime;
        public float CopyCreationTime => _CopyCreationTime;
        
    }
}