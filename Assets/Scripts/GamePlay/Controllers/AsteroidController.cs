using System;
using GamePlay.Configs;
using GamePlay.Game;
using GamePlay.Models;
using GamePlay.Views;
using UnityEngine;

namespace GamePlay.Controllers
{
    public class AsteroidController : IObjectController, IUpdatable
    {
        protected IObjectModel _model;
        protected IObjectView _view;

        protected float _speed;

        protected Camera _camera;
        
        protected bool _isDead;
        
        public event Action<IObjectController, IObjectView, bool> OnDie;

        public AsteroidController(IObjectModel model, IObjectView view)
        {
            _camera = Camera.main;
            
            InitSpeed();
            
            _model = model;
            _view = view;

            _view.OnCollision += OnCollision;
            _model.OnPositionChanged += HandlePositionChanged;
            
            SyncPosition();
            SyncRotation();
        }

        protected virtual void InitSpeed()
        {
            _speed = Game.Configs.Asteroids.Speed;
        }

        private void SyncRotation()
        {
            _view.Rotation = _model.Rotation;
        }

        private void HandlePositionChanged(Vector2 position)
        {
            SyncPosition();
        }

        private void SyncPosition()
        {
            _view.Position = _model.Position;
        }

        private void OnCollision(Collision2D other)
        {
            _view.OnCollision -= OnCollision;
            
            Die(false);
        }

        public void OnUpdate()
        {
            HandleMovement();
        }

        protected virtual void HandleMovement()
        {
            if (_isDead)
            {
                return;
            }
            
            _model.Position += _view.GetForward() * (_speed * Time.deltaTime);
            CheckBoundaries();
        }

        public void Die(bool silent)
        {
            if (!silent)
            {
                _isDead = true;
            }
            OnDie?.Invoke(this, _view, silent);
        }

        private void CheckBoundaries()
        {
            Vector2 viewportPosition = _camera.WorldToViewportPoint(_model.Position);

            if (viewportPosition.x < -0.3f || 
                viewportPosition.x > 1.3f || 
                viewportPosition.y < -0.3f ||
                viewportPosition.y > 1.3f)
            {
                Die(true);
            }
        }
    }
}