using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Entity.Scripts.Ai
{
    [CreateAssetMenu(fileName = "WaypointsDefinition", menuName = "WaypointsDefinition", order = 0)]
    public class WaypointsDefinition : SerializedScriptableObject
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField]
        private Dictionary<Transform, WaypointConfig> _WaypointByWeight;

        public Dictionary<Transform, WaypointConfig> WaypointByWeight => _WaypointByWeight;
    }

    public class WaypointConfig
    {
        public int Weight;
        public float Duration ;
        public int DayTime;

        public WaypointConfig(int weight, float duration, int dayTime)
        {
            Weight = weight;
            Duration = duration;
            DayTime = dayTime;
        }
    }
}
