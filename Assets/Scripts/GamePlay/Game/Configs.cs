using GamePlay.Configs;
using UnityEngine;

namespace GamePlay.Game
{
    public static class Configs
    {
        public static PlayerConfig Player;
        public static AsteroidConfig Asteroids;
        public static UFOConfig UFO;
        
        public static void LoadConfigs()
        {
            Player = Resources.Load<PlayerConfig>("PlayerConfig");
            Asteroids = Resources.Load<AsteroidConfig>("AsteroidConfig");
            UFO = Resources.Load<UFOConfig>("UFOConfig");
        }
    }
}