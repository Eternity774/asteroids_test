using System;
using GamePlay.Game;
using UnityEngine;

namespace Structure
{
    public class GameBootstrap : MonoBehaviour
    {
        private GameController _gameController;
        private void Awake()
        {
            _gameController = new GameController();
        }

        private void Update()
        {
            if (_gameController != null)
            {
                _gameController.OnUpdate();
            }
        }
    }
}
