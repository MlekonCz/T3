using UnityEngine;

namespace Entity.Scripts.Hand.Definitions
{
    public abstract class AHandUpgradeDefinition : ScriptableObject
    {
        
        [SerializeField] private int _Tier;
        public int Tier => _Tier;

        [SerializeField] private float _Cost;
        public float Cost => _Cost;
        
        [SerializeField] private float _Modifier;
        public float Modifier => _Modifier;
    }
}