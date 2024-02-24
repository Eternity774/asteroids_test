using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Configs
{
    [CreateAssetMenu(fileName = "UFOConfig", menuName = "Configs/UFOConfig")]
    public class UFOConfig : ScriptableObject
    {
        [SerializeField] private float _ufoSpeed;
        [SerializeField] private float _spawnDelay;
        
        public float UfoSpeed => _ufoSpeed;
        public float SpawnDelay => _spawnDelay;
    }
}