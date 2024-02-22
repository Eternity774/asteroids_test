using UnityEngine;

namespace GamePlay.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _rotationRate;
        [SerializeField] private float _shootCooldown;
        [SerializeField] private float _laserCooldown;

        public float MaxSpeed => _maxSpeed;
        public float Acceleration => _acceleration;
        public float Deceleration => _deceleration;
        public float RotationRate => _rotationRate;
        public float ShootCooldown => _shootCooldown;
        public float LaserCooldown => _laserCooldown;
    }
}
