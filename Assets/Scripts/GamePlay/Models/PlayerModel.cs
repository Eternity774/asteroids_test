using System;
using UnityEngine;

namespace GamePlay.Models
{
    public class PlayerModel : IObjectModel
    {
        private Vector2 _position;
        private Quaternion _rotation;
        private Vector2 _forward;
        
        public event Action<Vector2> OnPositionChanged;
        public event Action<Quaternion> OnRotationChanged;

        public Vector2 Position
        {
            get => _position;
            set
            {
                if (_position == value)
                {
                    return;
                }
                _position = value;
                OnPositionChanged?.Invoke(_position);
            }
        }

        public Vector2 Forward
        {
            get => _forward;
            set
            {
                if (_forward == value)
                {
                    return;
                }

                _forward = value;
            }
        }
        
        public Quaternion Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                OnRotationChanged?.Invoke(_rotation);
            }
        }

    }
}
