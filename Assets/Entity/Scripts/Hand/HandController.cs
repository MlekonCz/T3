using System;
using System.Collections.Generic;
using Entity.Scripts.Hand.Definitions;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Entity.Scripts.Hand
{
    public class HandController : MonoBehaviour
    {

        [SerializeField] private float _MovementSpeed = 150f;
        [SerializeField] private float _RotationSpeed = 75f;
        
        [SerializeField] private Rigidbody2D _Rigidbody2D;

        [SerializeField] private Animator _Animator;

        [SerializeField] private HandSignManager _HandSignManager;

        [SerializeField] private GameObject _CanvasRotator;
        public HandSignManager HandSignManager => _HandSignManager;

        
        private const string IS_WALKING = "IsWalking";
        private static readonly int IsWalking = Animator.StringToHash(IS_WALKING);

        private Vector2 _movement;
        private bool _isPickableInRange;

        private IPickable _pickableInRange;
        

        private float _multipliedSpeed;
        private float _multipliedRotation;


        private void Awake()
        {
            Game.Instance.PlayerManager.SignalOnPlayerUpgraded.AddListener(OnUpgradeCompleted);
        }
        private void OnDestroy()
        {
            
            Game.Instance.PlayerManager.SignalOnPlayerUpgraded.RemoveListener(OnUpgradeCompleted);

        }

        private void Start()
        {
            
            foreach (var upgrade in Game.Instance.PlayerManager.CurrentUpgrades())
            {
                if (upgrade is HandSpeedUpgrade speedUpg)
                {
                    SetModifiers(speedUpg);
                }
            }
        }

        
        private void OnUpgradeCompleted(List<AHandUpgradeDefinition> obj)
        {
            foreach (var upgrade in Game.Instance.PlayerManager.CurrentUpgrades())
            {
                if (upgrade is HandSpeedUpgrade speedUpg)
                {
                    SetModifiers(speedUpg);
                }
            }
        }
        
        private void SetModifiers(HandSpeedUpgrade handSpeedUpgrade)
        {
            _multipliedSpeed = _MovementSpeed * handSpeedUpgrade.Modifier;
            _multipliedRotation = _RotationSpeed * handSpeedUpgrade.Modifier;
        }
        
        private void Update()
        {
            UpdateMovement();
            
            // Calculate the opposite rotation of the player
            //Quaternion oppositeRotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.z / 2);

            // Apply the opposite rotation to the canvas
            //_CanvasRotator.transform.rotation = oppositeRotation;
            
            
            _CanvasRotator.transform.rotation = new Quaternion(0,0, - transform.rotation.eulerAngles.z,0);
            // CheckForPickingItemUp();
        }

        
        // private void CheckForPickingItemUp()
        // {
        //     if (_isPickableInRange && Input.GetKeyDown(KeyCode.F))
        //     {
        //         Pickable = _pickableInRange;
        //         _pickableInRange.OnPickedUp();
        //     }
        // }

      

        private void UpdateMovement()
        {
            float moveHorizontal = Input.GetAxis ("Horizontal");
            float moveVertical = Input.GetAxis ("Vertical");

         
            //transform.Rotate(0,0,moveHorizontal * _RotationSpeed * Time.deltaTime);
            _movement = new Vector2(moveVertical,0 ).normalized;
            Debug.Log(_movement.magnitude);
            _Animator.SetBool(IsWalking,_movement.magnitude > 0);
        }
        void FixedUpdate()
        {
            Vector2 direction = _Rigidbody2D.transform.TransformDirection(_movement);
            _Rigidbody2D.MoveRotation(_Rigidbody2D.rotation -= Input.GetAxis ("Horizontal")* _RotationSpeed * Time.deltaTime);
            _Rigidbody2D.velocity = direction * Time.deltaTime * _MovementSpeed;
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
