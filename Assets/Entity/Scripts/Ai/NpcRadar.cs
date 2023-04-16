using System;
using Entity.Scripts.Hand;
using Entity.Scripts.Utilities;
using UnityEngine;

namespace Entity.Scripts.Ai
{
    public class NpcRadar : MonoBehaviour
    {
        [SerializeField] private LayerMask _LayerMask;

        private float _tickSpeed;
        private bool _isInRange;

        private float _time = Mathf.Infinity;

        private float _suspicionIncrease;

        private Transform _player;

        private void Update()
        {
            if (_isInRange && _time >= _tickSpeed)
            {
                _time = 0;

                Vector3 direction = _player.position - transform.position;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, _LayerMask);

                if (hit.collider != null && hit.collider.CompareTag(TagManager.PLAYER))
                {
                    Game.Instance.GameManager.AddSuspicion(_suspicionIncrease);
                }
            }
            
            _time += Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(TagManager.PLAYER))
            {
                _player = col.transform;
                _isInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(TagManager.PLAYER))
            {
                _isInRange = false;
                _player = null;
            }
        }

        public void Initialize(float suspicionIncrease, float tickSpeed)
        {
            _suspicionIncrease = suspicionIncrease;
            _tickSpeed = tickSpeed;
        }
    }
}