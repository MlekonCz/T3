using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entity.Scripts.Ai
{
    public class NpcManager : MonoBehaviour
    {
        [SerializeField] private WaypointsDefinition _WaypointsDefinition;
        //public WaypointsDefinition WaypointsDefinition => _WaypointsDefinition;

    
    

        public Vector3 GetWaypoint(Dictionary<Transform, WaypointConfig> waypointsByWeight)
        {

            var availableWaypoints = new Dictionary<Transform, WaypointConfig>();
            foreach (var kvp in waypointsByWeight)
            {
                if (_WaypointsDefinition.WaypointByWeight.ContainsKey(kvp.Key))
                {
                    availableWaypoints.Add(kvp.Key,kvp.Value);
                }
            }

            return availableWaypoints.ElementAt(Random.Range(0,
                availableWaypoints.Count)).Key.position;

        }
        
        
        private Vector3 GetRandomWaypoint()
        {
            return _WaypointsDefinition.WaypointByWeight.ElementAt(Random.Range(0,
                _WaypointsDefinition.WaypointByWeight.Count)).Key.position;
        }
    }
}