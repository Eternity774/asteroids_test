using System.Collections.Generic;
using GamePlay.Controllers;
using GamePlay.Factories;
using GamePlay.Views;
using UnityEngine;

namespace GamePlay.Game
{
    public class GameController
    {
        private IObjectController _playerController;

        private List<IUpdatable> _updatables = new List<IUpdatable>();
        
        public GameController()
        {
            //creating player
            var playerFactory = new PlayerModelFactory();
            var playerModel = playerFactory.Create();

            playerModel.Position = Vector2.zero;
            playerModel.Rotation = Quaternion.identity;

            var playerViewFactory = new PlayerViewFactory();
            var playerView = playerViewFactory.Create();

            var playerControllerFactory = new PlayerControllerFactory();
            _playerController = playerControllerFactory.Create(playerModel, playerView);
            
            _updatables.Add((IUpdatable)_playerController);
        }

        public void OnUpdate()
        {
            if (_updatables != null && _updatables.Count > 0)
            {
                foreach (IUpdatable updatable in _updatables)
                {
                    updatable.OnUpdate();
                }
            }
        }
    }
}
