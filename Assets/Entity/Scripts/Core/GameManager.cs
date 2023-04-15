using UnityEngine;

namespace Entity.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private float _TimeMultiplier = 10;
        public float TimeMultiplier => _TimeMultiplier;
    
    }
}
