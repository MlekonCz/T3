using System;
using UnityEngine;

namespace Entity.Scripts.Ai
{
#if UNITY_EDITOR
    [ExecuteAlways]
#endif

    public class WaypointFinder : MonoBehaviour
    {
        private void Update()
        {

            
        }

        private void OnDrawGizmos()
        {
            
#if UNITY_EDITOR
            Gizmos.DrawSphere(transform.position,0.5f);
#endif
        }
    }
}