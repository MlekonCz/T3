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
                if (_WaypointsDefinition.WaypointConfigs.Find(x => x.Name == config.Name) != null)
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
                    return item.Position;
                }
                randomValue -= item.Weight;
            }

            return Vector3.zero; 
        }
    }
}