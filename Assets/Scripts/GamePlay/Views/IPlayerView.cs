using System;
using UnityEditor.iOS;
using UnityEngine;

namespace GamePlay.Views
{
    public interface IPlayerView : IObjectView
    {
        event Action<Vector2> OnMoveInput;
        event Action OnShootInput;
        event Action OnLaserInput;
    }
}
