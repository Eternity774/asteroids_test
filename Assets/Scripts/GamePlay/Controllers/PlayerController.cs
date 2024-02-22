using GamePlay.Configs;
using GamePlay.Models;
using GamePlay.Views;
using Unity.VisualScripting;
using UnityEngine;

namespace GamePlay.Controllers
{
    public class PlayerController : IObjectController, IUpdatable
    {
        private IObjectModel _model;
        private IPlayerView _view;

        private float _currentSpeed;

        private bool _isAccelerating;

        private Vector2 _currentInput;
        private Camera _camera;
        
        private PlayerConfig _config;

        public PlayerController(IObjectModel model, IPlayerView view)
        {
            _config = Resources.Load<PlayerConfig>("PlayerConfig");
            _camera = Camera.main;
            
            _model = model;
            _view = view;

            view.OnMoveInput += HandleMoveInput;
            view.OnShootInput += HandleShootInput;
            view.OnLaserInput += HandleLaserInput;
            
            model.OnPositionChanged += HandlePositionChanged;
            model.OnRotationChanged += HandleRotationChanged;
            
            SyncPosition();
            SyncRotation();
        }

        private void HandleLaserInput()
        {
            
        }

        private void HandleShootInput()
        {
            
        }

        private void HandleRotationChanged(Quaternion rotation)
        {
            SyncRotation();
        }

        private void HandlePositionChanged(Vector2 position)
        {
            SyncPosition();
        }

        private void HandleMoveInput(Vector2 input)
        {
            _currentInput = input;
            
        }
        
        public void OnUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            _isAccelerating = _currentInput.y > 0;
            
            if (!_isAccelerating)
            {
                _currentSpeed = Mathf.Clamp(_currentSpeed - _config.Deceleration * Time.deltaTime, 0, _config.MaxSpeed);
            }
            else
            {
                _currentSpeed = Mathf.Clamp(_currentSpeed + _config.Acceleration * Time.deltaTime, 0, _config.MaxSpeed);
            }
            
            _model.Position += _view.GetForward() * (_currentSpeed * Time.deltaTime);
            
            if (_currentInput.x != 0)
            {
                float zAngle = _model.Rotation.eulerAngles.z + _config.RotationRate * _currentInput.x;
                Quaternion target = Quaternion.Euler(0f, 0f, zAngle);
                _model.Rotation = Quaternion.Lerp(_model.Rotation, target, Time.deltaTime);
            }
            
            CheckBoundaries();
        }

        private void CheckBoundaries()
        {
            Vector2 screenModelPosition = _camera.WorldToScreenPoint(_model.Position);
            if (screenModelPosition.y > Screen.height)
            {
                float newPositionY = _camera.ScreenToWorldPoint(new Vector3(0f, 0, 0f)).y;
                _model.Position = new Vector2(_model.Position.x, newPositionY);
            }
            
            if (screenModelPosition.y < 0f)
            {
                float newPositionY = _camera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
                _model.Position = new Vector2(_model.Position.x, newPositionY);
            }

            if (screenModelPosition.x > Screen.width)
            {
                float newPositionX = _camera.ScreenToWorldPoint(new Vector3(0f, 0, 0f)).x;
                _model.Position = new Vector2(newPositionX, _model.Position.y);
            }
            
            if (screenModelPosition.x < 0f)
            {
                float newPositionX = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0f)).x;
                _model.Position = new Vector2(newPositionX, _model.Position.y);
            }
        }

        private void SyncPosition()
        {
            _view.Position = _model.Position;
        }

        private void SyncRotation()
        {
            _view.Rotation = _model.Rotation;
        }

        
    }
}