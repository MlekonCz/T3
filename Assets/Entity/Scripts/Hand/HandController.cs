using System;
using UnityEngine;

namespace Entity.Scripts.Hand
{
    public class HandController : MonoBehaviour
    {

        [SerializeField] private float _MovementSpeed;

        [SerializeField] private Rigidbody2D _Rigidbody2D;

        private bool _isPickableInRange;

        private IPickable _pickableInRange;
        
        public IPickable Pickable;
        private void Update()
        {
            UpdateRotation();
            UpdateMovement();
            CheckForPickingItemUp();
        }

        private void CheckForPickingItemUp()
        {
            if (_isPickableInRange && Input.GetKeyDown(KeyCode.F))
            {
                Pickable = _pickableInRange;
                _pickableInRange.OnPickedUp();
            }
        }

        private Vector2 _movement;
        private void UpdateMovement()
        {
            float moveHorizontal = Input.GetAxis ("Horizontal");
            float moveVertical = Input.GetAxis ("Vertical");
 
            _movement = new Vector2(moveVertical,moveHorizontal ).normalized;
            //_Rigidbody2D.velocity = new Vector2 (moveHorizontal, moveVertical)* Time.deltaTime * _MovementSpeed;
        
        }
        void FixedUpdate()
        {
            // Add velocity to the Rigidbody2D based on the movement vector and the object's rotation
            Vector2 direction = _Rigidbody2D.transform.TransformDirection(_movement);
            _Rigidbody2D.velocity = direction * Time.deltaTime * _MovementSpeed;
        }
       
        private void UpdateRotation()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;

            if (Camera.main != null)
            {
                Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
                mousePos.x -= objectPos.x;
                mousePos.y -= objectPos.y;
            }

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.TryGetComponent(out IPickable pickable)) return;
            _pickableInRange = pickable;
            _isPickableInRange = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out IPickable pickable)) return;
            _pickableInRange = null;
            _isPickableInRange = false;
        }

    }
}
