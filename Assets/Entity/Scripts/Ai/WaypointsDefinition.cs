
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Entity.Scripts
{
    [CreateAssetMenu(fileName = "WaypointsDefinition", menuName = "WaypointsDefinition", order = 0)]
    public class WaypointsDefinition : SerializedScriptableObject
    {
        [SerializeField]
        // ReSharper disable once InconsistentNaming
        private Dictionary<Transform, float> _WaypointByWeight;

        public Dictionary<Transform, float> WaypointByWeight => _WaypointByWeight;
    }
}
