using UnityEngine;

namespace GamePlay.Configs
{
    [CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Configs/AsteroidConfig")]
    public class AsteroidConfig : ScriptableObject
    {
        [SerializeField] private float _minSpawnDelay;
        [SerializeField] private float _maxSpawnDelay;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _smallAsteroidSpeed;

        public float Speed => Random.Range(_minSpeed, _maxSpeed);
        public float SpawnDelay => Random.Range(_minSpawnDelay, _maxSpawnDelay);
        public float SmallAsteroidSpeed => _smallAsteroidSpeed;
    }
}