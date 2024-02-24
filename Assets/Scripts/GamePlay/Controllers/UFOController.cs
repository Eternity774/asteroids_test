using System;
using GamePlay.Configs;
using GamePlay.Models;
using GamePlay.Views;
using UnityEngine;

namespace GamePlay.Controllers
{
    public class UFOController : AsteroidController
    {
        private IObjectModel _player;
        
        public UFOController(IObjectModel model, IObjectView view, IObjectModel player) : base(model, view)
        {
            _player = player;
        }

        protected override void InitSpeed()
        {
            _speed = Game.Configs.UFO.UfoSpeed;
        }

        protected override void HandleMovement()
        {
            if (_isDead)
            {
                return;
            }
            
            Vector2 direction = (_player.Position - _model.Position).normalized;
            _model.Position += direction * (_speed * Time.deltaTime);

        }
    }
}