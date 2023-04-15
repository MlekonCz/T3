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
                    availableWaypoints.Add(kvp.Key, kvp.Value);
                }
            }

            int totalWeight = 0;
            foreach (var item in availableWaypoints)
            {
                totalWeight += item.Value.Weight;
            }

            int randomValue = Random.Range(0, totalWeight);

            foreach (var item in availableWaypoints)
            {
                if (randomValue < item.Value.Weight)
                {
                    Debug.Log("weight of waypoint" + item.Value.Weight);

                    return item.Key.position;
                }

                Debug.Log("subtracted " + item.Value.Weight);
                randomValue -= item.Value.Weight;
            }

            return Vector3.zero; 
        }
    }
}