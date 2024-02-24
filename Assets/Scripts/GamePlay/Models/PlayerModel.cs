using System;
using UnityEngine;

namespace GamePlay.Models
{
    public class PlayerModel : IPlayerModel
    {
        private Vector2 _position;
        private Quaternion _rotation;
        private Vector2 _forward;
        private float _speed;
        private int _laserCharges;
        private float _laserCooldown;
        
        public event Action<Vector2> OnPositionChanged;
        public event Action<Quaternion> OnRotationChanged;
        public event Action<float> OnSpeedChanged;
        public event Action<int> OnLaserChargesChanged;
        public event Action<float> OnLaserCooldownChanged;
        

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

        public float Speed
        {
            get => _speed;
            set
            {
                if (_speed == value)
                {
                    return;
                }

                _speed = value;
                OnSpeedChanged?.Invoke(_speed);
            }
        }

        public int LaserCharges
        {
            get => _laserCharges;
            set
            {
                if (_laserCharges == value)
                {
                    return;
                }

                _laserCharges = value;
                OnLaserChargesChanged?.Invoke(_laserCharges);
            }
        }

        public float LaserCooldown
        {
            get => _laserCooldown;
            set
            {
                if (_laserCooldown == value)
                {
                    return;
                }

                _laserCooldown = value;
                OnLaserCooldownChanged?.Invoke(_laserCooldown);
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
