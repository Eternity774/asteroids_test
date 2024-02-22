using System;
using UnityEngine;

namespace GamePlay.Views
{
    public interface IObjectView
    {
        Vector2 Position { set; }
        Quaternion Rotation { set; }
        
        Vector2 GetForward();
    }
}