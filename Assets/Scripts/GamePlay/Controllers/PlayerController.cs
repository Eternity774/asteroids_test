using System;
using GamePlay.Configs;
using GamePlay.Factories;
using GamePlay.Game;
using GamePlay.Models;
using GamePlay.Views;
using UnityEngine;

namespace GamePlay.Controllers
{
    public class PlayerController : IPlayerController, IUpdatable
    {
        private const string Bullet = "Bullet";
        private const string Laser = "Laser";
        
        
        private IPlayerModel _model;
        private IPlayerView _view;

        private bool _isAccelerating;
        private bool _isDead;

        private float _shootTimer;

        private Vector2 _currentInput;
        private Camera _camera;
        
        private PlayerConfig _config;
        
        private ObjectViewFactory _bulletViewFactory;
        private DefaultModelFactory _modelFactory;
        private BulletControllerFactory _bulletControllerFactory;
        private LaserControllerFactory _laserControllerFactory;
        
        public event Action<IObjectController, IObjectView, bool> OnDie;

        public PlayerController(IPlayerModel model, IPlayerView view)
        {
            _config = Game.Configs.Player;
            _camera = Camera.main;
            
            _model = model;
            _view = view;

            _model.LaserCharges = _config.MaxLaserCharges;
            _model.LaserCooldown = _config.LaserCooldown;
            
            view.OnCollision += OnCollision;

            view.OnMoveInput += HandleMoveInput;
            view.OnShootInput += HandleShootInput;
            view.OnLaserInput += HandleLaserInput;
            
            model.OnPositionChanged += HandlePositionChanged;
            model.OnRotationChanged += HandleRotationChanged;
            
            SyncPosition();
            SyncRotation();
            
            InitBulletFactories();
            
            UpdateService.AddUpdateListener(this);
        }

        private void InitBulletFactories()
        {
            _modelFactory = new DefaultModelFactory();
            _bulletViewFactory = new ObjectViewFactory();
            _bulletControllerFactory = new BulletControllerFactory();
            _laserControllerFactory = new LaserControllerFactory();
        }

        private void Shoot()
        {
            var model = _modelFactory.Create();
            model.Position = _model.Position;
            model.Rotation = _model.Rotation;

            var view = _bulletViewFactory.Create(Bullet);
            var controller = _bulletControllerFactory.Create(model, view);
            controller.OnDie += OnBulletDie;
            
            UpdateService.AddUpdateListener((IUpdatable)controller);
        }

        private void LaserShoot()
        {
            var model = _modelFactory.Create();
            model.Position = _model.Position;
            model.Rotation = _model.Rotation;

            var view = _bulletViewFactory.Create(Laser);
            var controller = _laserControllerFactory.Create(model, view);
            controller.OnDie += OnBulletDie;
            
            UpdateService.AddUpdateListener((IUpdatable)controller);
        }

        private void OnBulletDie(IObjectController controller, IObjectView view, bool silent)
        {
            controller.OnDie -= OnBulletDie;
            
            _bulletViewFactory.SetViewFree(view);
            
            UpdateService.RemoveUpdateListener((IUpdatable)controller);

        }

        private void OnCollision(Collision2D other)
        {
            Die(false);
        }

        public void Die(bool silent)
        {
            _isDead = true;
            OnDie?.Invoke(this, _view, silent);
            _view.GetGameObject().SetActive(false);
        }

        public void Reset()
        {
            _isDead = false;

            _model.Position = Vector2.zero;
            _model.Rotation = Quaternion.identity;
            _model.Speed = 0f;
            _model.LaserCooldown = Game.Configs.Player.LaserCooldown;
            _model.LaserCharges = Game.Configs.Player.MaxLaserCharges;
            
            SyncPosition();
            SyncRotation();
            
            _view.GetGameObject().SetActive(true);
        }

        private void HandleLaserInput()
        {
            if (_model.LaserCharges > 0)
            {
                _model.LaserCharges--;
                
                LaserShoot();
            }
        }

        private void HandleShootInput()
        {
            if (_shootTimer <= 0)
            {
                Shoot();
                
                _shootTimer = _config.ShootCooldown;
            }
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
            ShootTimer();
            LaserTimer();
        }

        private void ShootTimer()
        {
            if (_shootTimer > 0)
            {
                _shootTimer -= Time.deltaTime;
            }
        }
        
        private void LaserTimer()
        {
            if (_model.LaserCharges < _config.MaxLaserCharges)
            {
                _model.LaserCooldown -= Time.deltaTime;
                if (_model.LaserCooldown <= 0)
                {
                    _model.LaserCharges++;
                    _model.LaserCooldown = _config.LaserCooldown;
                }
                
            }
        }

        private void HandleMovement()
        {
            if (_isDead)
            {
                return;
            }
            
            _isAccelerating = _currentInput.y > 0;
            
            if (!_isAccelerating)
            {
                _model.Speed = Mathf.Clamp(_model.Speed - _config.Deceleration * Time.deltaTime, 0, _config.MaxSpeed);
            }
            else
            {
                _model.Speed = Mathf.Clamp(_model.Speed + _config.Acceleration * Time.deltaTime, 0, _config.MaxSpeed);
            }
            
            _model.Position += _view.GetForward() * (_model.Speed * Time.deltaTime);
            
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