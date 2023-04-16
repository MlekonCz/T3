using System;
using System.Collections.Generic;
using Entity.Scripts.Hand;
using Entity.Scripts.Utilities;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Entity.Scripts.Ai
{
    public class NpcRadar : MonoBehaviour
    {
        [SerializeField] private LayerMask _LayerMask;

        private float _tickSpeed;
        private bool _isInRange;

        private bool _isItemInRange;

        private float _time = Mathf.Infinity;
        private float _itemTime = Mathf.Infinity;

        private float _suspicionIncrease;

        private Transform _player;

        private List<Transform> _items = new List<Transform>();

        public Signal<bool> SignalPlayerInRange = new Signal<bool>();

        private bool sentSignal;
        private void Update()
        {
            CheckForPlayer();
            CheckForItem();

            _time += Time.deltaTime;
            _itemTime += Time.deltaTime;
        }

        private void CheckForItem()
        {
            if (_isItemInRange && _itemTime >= _tickSpeed)
            {
                _itemTime = 0;

                foreach (var item in _items)
                {
                    Vector3 direction = item.position - transform.position;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, _LayerMask);
                    
                    if (hit.collider != null && hit.collider.transform == item)
                    {
                        var pickable = hit.collider.gameObject.GetComponent<IPickable>();
                        if (pickable.IsMissing())
                        {
                            Debug.Log("Item is missing");

                            Game.Instance.GameManager.AddSuspicion(pickable.GetItemTierDefinition().SusIncrease);
                        }
                    }
                }
               
            }
        }

        private void CheckForPlayer()
        {
            if (_isInRange && _time >= _tickSpeed)
            {

                _time = 0;

                Vector3 direction = _player.position - transform.position;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, _LayerMask);

                if (hit.collider != null && hit.collider.CompareTag(TagManager.PLAYER))
                {
                    if (!sentSignal)
                    {
                        SignalPlayerInRange.Dispatch(true);
                    }
                    Game.Instance.GameManager.AddSuspicion(_suspicionIncrease);
                    Game.Instance.PlayerManager.SetSign(true,Signs.Warning);
                }

                else if (hit.collider != null && hit.collider.CompareTag(TagManager.ITEM))
                {
                    Debug.Log("item is missing");
                    hit.collider.gameObject.TryGetComponent<IPickable>(out var pickable);
                    Game.Instance.GameManager.AddSuspicion(pickable.GetItemTierDefinition().SusIncrease);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(TagManager.PLAYER))
            {
                _player = col.transform;
                _isInRange = true;
            }
            else if (col.transform.parent != null && col.transform.parent.CompareTag(TagManager.ITEM))
            {
                col.transform.parent.TryGetComponent<IPickable>(out var pickable);
                if (pickable != null && pickable.IsMissing() && !_items.Contains(col.transform.parent))
                {
                    _items.Add(col.transform.parent);
                }
                _isItemInRange = _items.Count > 0;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(TagManager.PLAYER))
            {
                if (sentSignal)
                {
                    SignalPlayerInRange.Dispatch(false);
                }
                _isInRange = false;
                _player = null;
            }
            if (other.gameObject.CompareTag(TagManager.ITEM))
            {
                if (_items.Contains(other.transform))
                {
                    _items.Remove(other.transform);
                }

                Game.Instance.PlayerManager.SetSign(false,Signs.Warning);

            }
            else if (other.transform.parent != null && other.transform.parent.CompareTag(TagManager.ITEM))
            {
                if (_items.Contains(other.transform.parent))
                {
                    _items.Remove(other.transform.parent);
                }

                _isItemInRange = _items.Count > 0;
                _isItemInRange = _items.Count > 0;
            }
        }

        public void Initialize(float suspicionIncrease, float tickSpeed)
        {
            _suspicionIncrease = suspicionIncrease;
            _tickSpeed = tickSpeed;
        }
    }
}