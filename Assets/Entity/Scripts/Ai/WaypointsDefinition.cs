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
        
        [Button]
        private void Test()
        {
            foreach (var config in _WaypointConfigs)
            {
                config.Name = config.TranformForEditor.name;

                config.Position = config.TranformForEditor.position;
            }
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

        public Transform TranformForEditor;

        public WaypointConfig(String name,Vector3 position, int weight, float duration, int dayTime, Transform tranformForEditor)
        {
            Name = name;
            Position = position;
            Weight = weight;
            Duration = duration;
            DayTime = dayTime;
            TranformForEditor = tranformForEditor;
        }
    }
}