using System;
using System.Collections.Generic;
using GamePlay.Controllers;
using GamePlay.Factories;
using GamePlay.Models;
using GamePlay.Views;
using UnityEngine;

namespace GamePlay.Game
{
    public class GameController
    {
        private IPlayerController _playerController;
        private IPlayerModel _playerModel;

        private EnemySpawner _enemySpawner;

        private UIController _uiController;
        private UpdateService _updateService;

        private GameModel _model;

        public GameModel Model => _model;
        
        public event Action OnGameFinished;
        
        public GameController()
        {
            _model = new GameModel();
            
            Configs.LoadConfigs();
            
            _updateService = new UpdateService();

            CreatePlayer();
            CreateAsteroidSpawner();
            
            _uiController = new UIController(this, _playerModel);
            _uiController.OnRestartButtonClicked += RestartGame;
        }

        private void RestartGame()
        {
            _enemySpawner.Restart();
            _playerController.Reset();

            _model.IsGameFinished = false;
            _model.Points = 0;
        }

        private void CreateAsteroidSpawner()
        {
            _enemySpawner = new EnemySpawner(_playerModel, _model);
        }

        private void CreatePlayer()
        {
            //creating player
            var playerFactory = new PlayerModelFactoryBase();
            _playerModel = (IPlayerModel)playerFactory.Create();

            _playerModel.Position = Vector2.zero;
            _playerModel.Rotation = Quaternion.identity;

            var prefab = Resources.Load<GameObject>("Player");
            var playerView = GameObject.Instantiate(prefab).GetComponent<IPlayerView>();

            _playerController = new PlayerController(_playerModel, playerView);
            _playerController.OnDie += OnPlayerDie;
        }

        private void OnPlayerDie(IObjectController playerController, IObjectView objectView, bool silent)
        {
            _model.Points -= 100;
            _model.IsGameFinished = true;
            
            OnGameFinished?.Invoke();
        }

        public void OnUpdate()
        {
            if (_updateService != null)
            {
                _updateService.OnUpdate();
            }
        }
    }
}
