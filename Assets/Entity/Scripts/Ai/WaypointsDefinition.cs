using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Entity.Scripts.Ai
{
    [CreateAssetMenu(fileName = "WaypointsDefinition", menuName = "WaypointsDefinition", order = 0)]
    public class WaypointsDefinition : SerializedScriptableObject
    {
        [SerializeField] private List<WaypointConfig> _WaypointConfigs = new List<WaypointConfig>();

        public List<WaypointConfig> WaypointConfigs => _WaypointConfigs;


     
        [Button]
        private void Reorder()
        {
            _WaypointConfigs.Sort((p1, p2) =>
                String.Compare(p1.Name, p2.Name, StringComparison.Ordinal));
        }
    }

    [Serializable]
    public class WaypointConfig
    {
        public String Name;
        public Vector3 Position;
        public int Weight;
        public float Duration;
        public int DayTime;

        public WaypointConfig(String name,Vector3 position, int weight, float duration, int dayTime)
        {
            Name = name;
            Position = position;
            Weight = weight;
            Duration = duration;
            DayTime = dayTime;
        }
    }
}