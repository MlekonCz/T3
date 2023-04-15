using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Entity.Scripts.Ai
{
    public class NpcController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _NavMeshAgent;

        
        [SerializeField] private WaypointsDefinition _WaypointsDefinition;

        
        
        void Start()	{
            
            _NavMeshAgent.updateRotation = false;
            _NavMeshAgent.updateUpAxis = false;
            _NavMeshAgent.destination = GetRandomWaypoint();
        }

        private Vector3 GetRandomWaypoint()
        {
            return Game.Instance.NpcManager.GetWaypoint(_WaypointsDefinition.WaypointByWeight);
        }


        private void Update()
        {
            if (Vector3.Distance(_NavMeshAgent.destination,transform.position) < 0.2f )
            {
                _NavMeshAgent.destination =  GetRandomWaypoint();
            }
        }
    }
}