using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GamePlay.Views
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public event Action<Vector2> OnMoveInput;
        public event Action OnShootInput;
        public event Action OnLaserInput;

        public Vector2 Position {
            set
            {
                transform.position = value;
            }
        }

        public Quaternion Rotation
        {
            set
            {
                transform.rotation = value;
            }
        }
        
        private InputActionAsset _inputActionAsset;
        private InputAction _moveAction;
        private InputAction _shootAction;
        private InputAction _laserAction;

        private void Awake()
        {
            _inputActionAsset = Resources.Load<InputActionAsset>("DefaultInputActions");

            InitMoveInput();
            InitShootInput();
            InitLaserInput();
        }
        
        public Vector2 GetForward()
        {
            return transform.up;
        }

        private void InitMoveInput()
        {
            _moveAction = _inputActionAsset.FindAction("Move");

            if (_moveAction != null)
            {
                _moveAction.started += OnMoveAction;
                _moveAction.performed += OnMoveAction;
                _moveAction.canceled += OnMoveAction;
            
                _moveAction.Enable();    
            }
        }
        
        private void DeInitMoveInput()
        {
            if (_moveAction != null)
            {
                _moveAction.Disable();
            
                _moveAction.started -= OnMoveAction;
                _moveAction.performed -= OnMoveAction;
                _moveAction.canceled -= OnMoveAction;
            }
        }
        
        private void InitShootInput()
        {
            _shootAction = _inputActionAsset.FindAction("Shoot");

            if (_shootAction != null)
            {
                _shootAction.started += OnShootAction;
                _shootAction.performed += OnShootAction;
                _shootAction.canceled += OnShootAction;
            
                _shootAction.Enable();    
            }
        }
        
        private void DeInitShootInput()
        {
            if (_shootAction != null)
            {
                _shootAction.Disable();
            
                _shootAction.started -= OnShootAction;
                _shootAction.performed -= OnShootAction;
                _shootAction.canceled -= OnShootAction;
            }
        }
        
        private void InitLaserInput()
        {
            _laserAction = _inputActionAsset.FindAction("Laser");

            if (_laserAction != null)
            {
                _laserAction.started += OnLaserAction;
                _laserAction.performed += OnLaserAction;
                _laserAction.canceled += OnLaserAction;
            
                _laserAction.Enable();    
            }
        }
        
        private void DeInitLaserInput()
        {
            if (_laserAction != null)
            {
                _laserAction.Disable();
            
                _laserAction.started -= OnLaserAction;
                _laserAction.performed -= OnLaserAction;
                _laserAction.canceled -= OnLaserAction;
            }
        }

        private void OnMoveAction(InputAction.CallbackContext context)
        {
            if (context.started || context.performed)
            {
                OnMoveInput?.Invoke(context.ReadValue<Vector2>());
            }

            if (context.canceled)
            {
                OnMoveInput?.Invoke(Vector2.zero);
            }
        }
        
        private void OnShootAction(InputAction.CallbackContext context)
        {
            
        }
        
        private void OnLaserAction(InputAction.CallbackContext context)
        {
            
        }

        private void OnDestroy()
        {
            DeInitMoveInput();
            DeInitShootInput();
            DeInitLaserInput();
        }
    }
}