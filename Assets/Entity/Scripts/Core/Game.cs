using System;
using Entity.Scripts.Ai;
using UnityEngine;

namespace Entity.Scripts
{
    public  class Game : MonoBehaviour
    {
        
        private static Game _instance;
        public static Game Instance => _instance;

        [SerializeField] private NpcManager _NpcManager;
        public NpcManager NpcManager => _NpcManager;
        
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        
    }
}