using System;
using GamePlay.Game;
using UI;
using UnityEngine;

namespace GamePlay.Controllers
{
    public class LoseScreenController : IUIController
    {
        private LoseScreenView _view;
        
        public event Action OnRestartButtonClicked;

        public LoseScreenController(LoseScreenView view, GameModel gameModel)
        {
            _view = view;
            _view.Points.text = gameModel.Points.ToString();

            _view.OnRestartButtonClicked += OnRestart;
        }

        private void OnRestart()
        {
            _view.OnRestartButtonClicked -= OnRestart;
            
            OnRestartButtonClicked?.Invoke();
        }

        public void Close()
        {
            if (_view != null)
            {
                GameObject.Destroy(_view.gameObject);
            }
        }
    }
}