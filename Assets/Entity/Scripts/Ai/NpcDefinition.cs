using UnityEngine;

namespace Entity.Scripts.Ai
{
    [CreateAssetMenu(fileName = "NpcDefinition", menuName = "NpcDefinition", order = 0)]
    public class NpcDefinition : ScriptableObject
    {
        [SerializeField]
        public float _SuspicionIncrease;

        public float SuspicionIncrease => _SuspicionIncrease;

        
        [SerializeField]
        public float _SpeedMultiplier;

        public float SpeedMultiplier => _SpeedMultiplier;
        
        [SerializeField] private float _SusIncreaseSpeed = 0.5f;
        public float SusIncreaseSpeed => _SusIncreaseSpeed;
    }
}