using UnityEngine;

namespace Entity.Scripts
{
    [CreateAssetMenu(fileName = "ItemTier", menuName = "ItemTier", order = 0)]
    public class ItemTierDefinition : ScriptableObject
    {
        [SerializeField] private int _Tier;
        public int Tier => _Tier;
        
        [SerializeField] private float _Reward;
        public float Reward => _Reward;

        [SerializeField] private float _SlowModifier;
        public float SlowModifier => _SlowModifier;

        [SerializeField] private float _PickUpTime;
        public float PickUpTime => _PickUpTime;

        [SerializeField] private float _CopyCreationTime;
        public float CopyCreationTime => _CopyCreationTime;
    }
}