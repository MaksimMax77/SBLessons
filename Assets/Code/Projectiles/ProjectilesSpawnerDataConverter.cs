using Unity.Entities;
using UnityEngine;

namespace Code.Projectiles
{
    public class ProjectilesSpawnerDataConverter : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private Projectile _prefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _spawnDelay;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new ProjectilesSpawnerData
            {
                prefab = _prefab,
                spawnPoint = _spawnPoint,
                spawnDelay = _spawnDelay
            });
        }
    }

    public class ProjectilesSpawnerData : IComponentData
    {
        public Projectile prefab;
        public Transform spawnPoint;
        public float spawnDelay;
    }
}
