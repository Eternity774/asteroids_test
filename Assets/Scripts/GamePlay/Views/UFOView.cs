﻿using UnityEngine;

namespace GamePlay.Views
{
    public class UFOView : MonoBehaviour, IObjectView
    {
        public Vector2 Position { get; set; }
        public Quaternion Rotation { get; set; }
        
        public Vector2 GetForward()
        {
            return transform.forward;
        }
    }
}