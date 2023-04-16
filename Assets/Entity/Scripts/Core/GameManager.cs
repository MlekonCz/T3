using strange.extensions.signal.impl;
using UnityEngine;

namespace Entity.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _TimeMultiplier = 10;
        public float TimeMultiplier => _TimeMultiplier;

        [SerializeField] private float _MaxSuspicion = 100;
        public float MaxSuspicion => _MaxSuspicion;
        private float _suspicion;
        public float Suspicion => _suspicion;

        public Signal<float> SignalOnSuspicionChanged = new Signal<float>();


        public void AddSuspicion(float value)
        {
            _suspicion += value;
            SignalOnSuspicionChanged.Dispatch(_suspicion);
            Debug.Log("Added sus, total: " + _suspicion);
        }

    }
}