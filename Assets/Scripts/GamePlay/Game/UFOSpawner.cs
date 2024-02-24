using GamePlay.Configs;
using GamePlay.Models;
using GamePlay.Views;

namespace GamePlay.Game
{
    public class UFOSpawner : IUpdatable
    {
        private const string UFO = "UFO";

        private IObjectModel _playerModel;

        private UFOConfig _ufoConfig;
        
        public UFOSpawner(IObjectModel playerModel)
        {
            _playerModel = playerModel;
        }
        
        public void OnUpdate()
        {
            
        }
    }
}