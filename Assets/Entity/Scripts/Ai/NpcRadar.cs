using System;
using Entity.Scripts.Hand;
using Entity.Scripts.Utilities;
using UnityEngine;

namespace Entity.Scripts.Ai
{
    public class NpcRadar : MonoBehaviour
    {

        private float _tickSpeed;
        private bool _isInRange;

        private float _time = Mathf.Infinity;

        private float _suspicionIncrease;
        private void Update()
        {
            if (_isInRange && _time >= _tickSpeed)
            {
                _time = 0;
                Game.Instance.GameManager.AddSuspicion(_suspicionIncrease);
            }

            _time += Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(TagManager.PLAYER))
            {
                _isInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(TagManager.PLAYER))
            {
                _isInRange = false;

            }
        }

        public void Initialize(float suspicionIncrease, float tickSpeed)
        {
            _suspicionIncrease = suspicionIncrease;
            _tickSpeed = tickSpeed;
        }
    }
}