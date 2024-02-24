using System;
using GamePlay.Models;
using GamePlay.Views;
using UnityEngine;

namespace GamePlay.Controllers
{
    public class LaserController : IObjectController, IUpdatable
    {
        private IObjectModel _model;
        private IObjectView _view;

        private float _activeTimer = 0.2f;

        private bool _isDead;
        
        public event Action<IObjectController, IObjectView, bool> OnDie;
        
        public void Die(bool silent)
        {
            if (!silent)
            {
                _isDead = true;
            }
            OnDie?.Invoke(this, _view, silent);
        }

        public void Clear()
        {
            
        }

        public LaserController(IObjectModel model, IObjectView view)
        {
            _model = model;
            _view = view;
            
            SyncPosition();
            SyncRotation();
        }
        
        private void SyncRotation()
        {
            _view.Rotation = _model.Rotation;
        }
        
        private void SyncPosition()
        {
            _view.Position = _model.Position;
        }
        
        public void OnUpdate()
        {
            if (_isDead)
            {
                return;
            }
            
            _activeTimer -= Time.deltaTime;
            if (_activeTimer <= 0)
            {
                _isDead = true;
                OnDie?.Invoke(this, _view, false);
            }
        }
    }
}