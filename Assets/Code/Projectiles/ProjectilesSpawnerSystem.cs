using Code.Input;
using Code.Pool;
using Code.Utils;
using Unity.Entities;

namespace Code.Projectiles
{
    public class ProjectilesSpawnerSystem : ComponentSystem
    {
        private EntityQuery _query;
        private EntityQuery _poolQuery;
        private ObjectsPool _objectsPool;
        private Timer _timer;

        protected override void OnCreate()
        {
            _query = GetEntityQuery(ComponentType.ReadOnly<ProjectilesSpawnerData>(), ComponentType.ReadOnly<InputData>());
            _poolQuery = GetEntityQuery(ComponentType.ReadOnly<ObjectsPool>());
            _timer = new Timer();
        }

        protected override void OnUpdate()
        {
            SetPool();
            
            Entities.With(_query).ForEach((Entity entity, ProjectilesSpawnerData spawnerData, ref InputData inputData) =>
            {
                _timer.Init(spawnerData.spawnDelay);

                if (!inputData.isShootButtonClick)
                {
                    _timer.SetMaxValue();
                    return;
                }

                _timer.Update(Time.DeltaTime);

                if (!_timer.IsEnd)
                {
                    return;
                }
                
                var projectile = _objectsPool.GetOrCreateObject(spawnerData.prefab);
                projectile.transform.position = spawnerData.spawnPoint.position;
                projectile.AddForce(spawnerData.spawnPoint.forward);
                _timer.Restart();
            });
        }

        private void SetPool()
        {
            if (_objectsPool != null)
            {
                return;
            }
            
            Entities.With(_poolQuery).ForEach((Entity entity, ObjectsPool objectsPool) =>
            {
                _objectsPool = objectsPool;
            });
        }
    }
}
