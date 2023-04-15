using System;
using System.Collections.Generic;
using Entity.Scripts.Hand.Definitions;
using UnityEngine;

namespace Entity.Scripts.Hand
{
    public class HandController : MonoBehaviour
    {

        [SerializeField] private float _MovementSpeed = 150f;
        [SerializeField] private float _RotationSpeed = 75f;

        [SerializeField] private Rigidbody2D _Rigidbody2D;

        [SerializeField] private Animator _Animator;

        private const string IS_WALKING = "IsWalking";
        private static readonly int IsWalking = Animator.StringToHash(IS_WALKING);

        private Vector2 _movement;
        private bool _isPickableInRange;

        private IPickable _pickableInRange;
        
        public IPickable Pickable;


        private void Start()
        {
            
        }

        public void Initialize(HandSpeedUpgrade handSpeedUpgrade)
        {
            _MovementSpeed *= handSpeedUpgrade.Modifier;
            _RotationSpeed *= handSpeedUpgrade.Modifier;
        }
        
        private void Update()
        {
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

      

        private void UpdateMovement()
        {
            float moveHorizontal = Input.GetAxis ("Horizontal");
            float moveVertical = Input.GetAxis ("Vertical");

         
            //transform.Rotate(0,0,moveHorizontal * _RotationSpeed * Time.deltaTime);
            _movement = new Vector2(moveVertical,0 ).normalized;
            
            _Animator.SetBool(IsWalking,_movement.magnitude != 0);
        }
        void FixedUpdate()
        {
            Vector2 direction = _Rigidbody2D.transform.TransformDirection(_movement);
            _Rigidbody2D.MoveRotation(_Rigidbody2D.rotation -= Input.GetAxis ("Horizontal")* _RotationSpeed * Time.deltaTime);
            _Rigidbody2D.velocity = direction * Time.deltaTime * _MovementSpeed;
        }
       /* //old rotation towards mouse
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
*/
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
