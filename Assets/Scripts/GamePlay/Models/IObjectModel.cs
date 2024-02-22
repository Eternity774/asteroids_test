using System;
using UnityEngine;

namespace GamePlay.Models
{
    public interface IObjectModel
    {
        event Action<Vector2> OnPositionChanged;
        event Action<Quaternion> OnRotationChanged;
        
        Vector2 Position { get; set; }
        Vector2 Forward { get; set; }
        Quaternion Rotation { get; set; }
    }
}
