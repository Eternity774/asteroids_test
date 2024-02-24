using System;
using GamePlay.Controllers;
using GamePlay.Models;
using UI;
using UnityEngine;

namespace GamePlay.Game
{
    public class UIController : IService
    {
        private const string Canvas = "MainCanvas";
        private const string ShipStatView = "ShipStats";
        private const string LoseScreen = "LoseScreen";
        
        private Canvas _canvas;

        private IPlayerModel _playerModel;
        private GameModel _gameModel;
        
        private IUIController _shipStatController;
        private IUIController _loseScreenController;

        public event Action OnRestartButtonClicked;

        public UIController(GameController gameController, IPlayerModel playerModel)
        {
            _playerModel = playerModel;
            
            gameController.OnGameFinished += OnGameFinished;
            _gameModel = gameController.Model;
            
            CreateCanvas();
            CreateShipStat();
        }

        private void CreateCanvas()
        {
            var canvasPrefab = Resources.Load<GameObject>(Canvas);
            _canvas = GameObject.Instantiate(canvasPrefab).GetComponent<Canvas>();
        }

        private void CreateShipStat()
        {
            var model = new ShipStatModel();

            var viewPrefab = Resources.Load<GameObject>(ShipStatView);
            var view = GameObject.Instantiate(viewPrefab, _canvas.transform).GetComponent<ShipStatsView>();

            _shipStatController = new ShipStatController(model, view, _playerModel);
        }

        private void CreateLoseScreen()
        {
            var viewPrefab = Resources.Load<GameObject>(LoseScreen);
            var view = GameObject.Instantiate(viewPrefab, _canvas.transform).GetComponent<LoseScreenView>();

            var controller = new LoseScreenController(view, _gameModel);
            controller.OnRestartButtonClicked += OnRestart;
            _loseScreenController = controller;
        }

        private void OnRestart()
        {
            if (_loseScreenController != null)
            {
                _loseScreenController.Close();
            }
            
            OnRestartButtonClicked?.Invoke();
        }

        private void OnGameFinished()
        {
            CreateLoseScreen();
        }

        public void OnUpdate()
        {
            
        }
    }
}