using Code.Health;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;

namespace Code.AiUtilitySystem.Behaviors.AiUnitBehaviors
{
    public abstract class AliveTargetOrientedBehavior<T>  : BehaviorData.Behavior<T> where T : BehaviorData
    {
        protected Entity[] _targets;
        protected EntityManager _entityManager;
        protected Transform _behaviorEntityTransform; 
        protected NavMeshAgent _navMeshAgent;

        protected AliveTargetOrientedBehavior(T behaviorData, Entity aiAgentEntity) : base(behaviorData, aiAgentEntity)
        {
            SetDependencies();
            CollectTargets();
        }

        private void SetDependencies()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _behaviorEntityTransform = _entityManager.GetComponentObject<Transform>(_aiAgentEntity);
            _navMeshAgent = _entityManager.GetComponentObject<NavMeshAgent>(_aiAgentEntity);
        }

        private void CollectTargets()
        {
            _targets = _entityManager
                .CreateEntityQuery(ComponentType.ReadOnly<ObjectHealth>())
                .ToEntityArray(Allocator.Temp).ToArray();
        }

        protected float CalculateDistanceToTarget(Transform targetTransform)
        {
            return (targetTransform.position - _behaviorEntityTransform.position).magnitude;
        }
    }
}
