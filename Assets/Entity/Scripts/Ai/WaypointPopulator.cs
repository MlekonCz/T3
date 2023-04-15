using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Entity.Scripts.Ai
{
    public class WaypointPopulator : MonoBehaviour
    {
        [SerializeField] private WaypointsDefinition _WaypointsDefinition;


        [SerializeField] private Transform waypoints;
        [Button]
        private void Populate()
        {
            var test = FindObjectsOfType<WaypointFinder>();

            foreach (var target in test)
            {
                _WaypointsDefinition.WaypointByWeight.Add(target.transform, new WaypointConfig(1, 1, -1));
            }
        }

        [SerializeField] private Transform _Target;
        [SerializeField] private GameObject _Prefab;
        [Button]
        private void GenerateWaypoints(int count, string name)
        {
            var test = FindObjectsOfType<WaypointFinder>();

            for (int i = 1; i <= count; i++)
            {
                var waypointName = $"{test.Length + i}_{name}";
                var waypoint = Instantiate(_Prefab, _Target);
                waypoint.name = waypointName;
            }
            
        }
    }
}