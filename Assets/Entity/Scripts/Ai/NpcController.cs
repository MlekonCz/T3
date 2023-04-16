using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Entity.Scripts.Ai
{
    public class NpcController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _NavMeshAgent;
        
        [SerializeField] private WaypointsDefinition _WaypointsDefinition;

        [SerializeField] private Animator _Animator;

        [SerializeField] private NpcDefinition _NpcDefinition;

        [SerializeField] private NpcRadar _NpcRadar;
        
        private WaypointConfig _currentWaypoint;

        private float _waypointDuration;

        private bool _isAtDestination = false;

        private const string IS_WALKING = "IsWalking";
        private static readonly int IsWalking = Animator.StringToHash(IS_WALKING);

        private float sped;
        void Start()	{
            
            _NavMeshAgent.updateRotation = false;
            _NavMeshAgent.updateUpAxis = false;
            _NavMeshAgent.destination = GetRandomWaypoint();
            _NpcRadar.Initialize(_NpcDefinition._SuspicionIncrease, _NpcDefinition.SusIncreaseSpeed);
            _NavMeshAgent.speed *= _NpcDefinition._SpeedMultiplier;
            _NpcRadar.SignalPlayerInRange.AddListener(PlayerInRange);
            sped = _NavMeshAgent.speed;
        }

        private void OnDestroy()
        {
            
            _NpcRadar.SignalPlayerInRange.RemoveListener(PlayerInRange);

        }

        private void PlayerInRange(bool obj)
        {
            _NavMeshAgent.speed = obj ? 0.01f : sped;

        }

        private Vector3 GetRandomWaypoint()
        {
            _currentWaypoint = Game.Instance.NpcManager.GetWaypoint(_WaypointsDefinition.WaypointConfigs, out _waypointDuration);
            return _currentWaypoint.Position;
        }


        private void Update()
        {
            if (_Animator)
            {
                _Animator.SetBool(IsWalking,!_isAtDestination);
            }

            
            if (Vector3.Distance(_NavMeshAgent.destination,transform.position) < 0.2f && !_isAtDestination )
            {
                _isAtDestination = true;
                Invoke(nameof(SetNewDestination),_waypointDuration * Game.Instance.GameManager.TimeMultiplier);
            }

            if (!_isAtDestination && _Animator)
            {
                float angle = Mathf.Atan2(_NavMeshAgent.velocity.y, _NavMeshAgent.velocity.x);

                float degrees = angle * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, degrees);
            }
        }

        private void SetNewDestination()
        {
            _isAtDestination = false;
            _NavMeshAgent.destination =  GetRandomWaypoint();
            Game.Instance.NpcManager.ReturnWaypoint(_currentWaypoint);
        }

    }
}