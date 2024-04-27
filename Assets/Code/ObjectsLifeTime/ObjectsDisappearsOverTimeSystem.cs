using System.Collections.Generic;
using System.Linq;
using Code.Pool;
using Code.Utils;
using Unity.Entities;

namespace Code.ObjectsLifeTime
{
    public class ObjectsDisappearsOverTimeSystem : ComponentSystem
    {
        private EntityQuery _objectQuery;
        private EntityQuery _poolQuery;
        private ObjectsPool _objectsPool;
        private List<int> _deathObjectsIndexes = new();
        private Dictionary<DisappearObjectData, Timer> _timers = new();
        
        protected override void OnCreate()
        {
            _objectQuery = GetEntityQuery(ComponentType.ReadOnly<DisappearObjectData>());
            _poolQuery = GetEntityQuery(ComponentType.ReadOnly<ObjectsPool>());
        }

        protected override void OnUpdate()
        {
            SetPool();
            CollectAliveObjectAndCreateTimers();
            UpdateTimersAndCollectDeathIndexes();
            DisableAndRemoveDeathObjects();
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

        private void CollectAliveObjectAndCreateTimers()
        {
            Entities.With(_objectQuery).ForEach((Entity entity, DisappearObjectData disappearObject) =>
            {
                if (!disappearObject.disappearingObject.gameObject.activeInHierarchy)
                {
                    return;
                }

                var timer = new Timer();
                timer.Init(disappearObject.lifeTime);
                _timers.TryAdd(disappearObject, timer);
            });
        }
        
        private void UpdateTimersAndCollectDeathIndexes()
        {
            var len = _timers.Count;
            for (var i = 0; i < len; ++i)
            {
                var timer = _timers.Values.ToArray()[i];
                timer.Update(Time.DeltaTime);
                
                if (!timer.IsEnd)
                {
                    continue;
                }
                
                _deathObjectsIndexes.Add(i);
            }
        }

        private void DisableAndRemoveDeathObjects()
        {
            for (int i = 0, len = _deathObjectsIndexes.Count; i < len; ++i)
            {
                var objToDisableIndex = _deathObjectsIndexes[i];
                var objToDisable = _timers.Keys.ToArray()[objToDisableIndex];
                _timers.Remove(objToDisable);
                _objectsPool.PutAndDisable(objToDisable.disappearingObject);
            }
            
            _deathObjectsIndexes.Clear();
        }
    }
}
