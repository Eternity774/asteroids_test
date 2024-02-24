using System;
using UnityEngine;

namespace GamePlay.Views
{
    public interface IObjectView
    {
        event Action<Collision2D> OnCollision;
        Vector2 Position { get; set; }
        Quaternion Rotation { set; }

        GameObject GetGameObject();
        Vector2 GetForward();
    }
}