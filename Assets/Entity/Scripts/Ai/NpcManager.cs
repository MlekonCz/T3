using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entity.Scripts.Ai
{
    public class NpcManager : MonoBehaviour
    {
        [SerializeField] private WaypointsDefinition _WaypointsDefinition;
        //public WaypointsDefinition WaypointsDefinition => _WaypointsDefinition;


        public Vector3 GetWaypoint(List<WaypointConfig> waypointsByWeight)
        {
            var availableWaypoints = new List<WaypointConfig>();
            foreach (var config in waypointsByWeight)
            {
                if (_WaypointsDefinition.WaypointConfigs.Contains(config))
                {
                    availableWaypoints.Add(config);
                }
            }

            int totalWeight = 0;
            foreach (var item in availableWaypoints)
            {
                totalWeight += item.Weight;
            }

            int randomValue = Random.Range(0, totalWeight);

            foreach (var item in availableWaypoints)
            {
                if (randomValue < item.Weight)
                {
                    Debug.Log("weight of waypoint" + item.Weight);

                    return item.Position;
                }

                Debug.Log("subtracted " + item.Weight);
                randomValue -= item.Weight;
            }

            return Vector3.zero; 
        }
    }
}