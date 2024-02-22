using GamePlay.Views;
using UnityEngine;

namespace GamePlay.Factories
{
    public class PlayerViewFactory
    {
        public IPlayerView PlayerView { get; private set; }

        public IPlayerView Create()
        {
            var prefab = Resources.Load<GameObject>("Player");
            PlayerView = GameObject.Instantiate(prefab).GetComponent<IPlayerView>();
            return PlayerView;
        }
    }
}