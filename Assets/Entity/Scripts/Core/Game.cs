using System;
using Entity.Scripts.Ai;
using Entity.Scripts.Hand;
using UnityEngine;

namespace Entity.Scripts
{
    public  class Game : MonoBehaviour
    {
        
        private static Game _instance;
        public static Game Instance => _instance;

        [SerializeField] private NpcManager _NpcManager;
        public NpcManager NpcManager => _NpcManager;
        
        [SerializeField] private PlayerManager _PlayerManager;
        public PlayerManager PlayerManager => _PlayerManager;

        
        
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