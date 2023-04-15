using System;
using UnityEngine;
using UnityEngine.AI;

namespace Entity.Scripts
{
    public class NpcController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _NavMeshAgent;

        [SerializeField] private Transform _Player;
        
        void Start()	{
            
            _NavMeshAgent.updateRotation = false;
            _NavMeshAgent.updateUpAxis = false;
        }


        private void Update()
        {
            GetComponent<NavMeshAgent>().destination = _Player.position;
        }
    }
}