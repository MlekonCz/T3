using System;
using UnityEngine;

namespace Entity.Scripts
{
    public  class Game : MonoBehaviour
    {
        
        private static Game _instance;
        public static Game Instance => _instance;


        
        
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