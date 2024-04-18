using Code.Health;
using Unity.Entities;
using UnityEngine;

namespace Code.AiUtilitySystem.Behaviors.AiUnitBehaviors
{
    [CreateAssetMenu(menuName = "Behaviors/" + nameof(ChaseBehaviorData), fileName = nameof(ChaseBehaviorData))]
    public class ChaseBehaviorData : BehaviorData
    {
        [SerializeField] private float _minDistance;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _speed;

        public override IBehavior CreateBehavior(Entity aiAgentEntity)
        {
            return new ChaseBehavior(this, aiAgentEntity);
        }
        
        private class ChaseBehavior: AliveTargetOrientedBehavior<ChaseBehaviorData>
        {
            private Transform _currentTargetTransform;
            
            public ChaseBehavior(ChaseBehaviorData behaviorData, Entity aiAgentEntity) : base(behaviorData, aiAgentEntity)
            {
            }

            public override float Evaluate()
            {
                for (int i = 0, len = _targets.Length; i < len; ++i)
                {
                    var targetEntity = _targets[i];
                    var targetHealth  = _entityManager.GetComponentObject<ObjectHealth>(targetEntity);
                    var targetTransform = _entityManager.GetComponentObject<Transform>(targetEntity);

                    var distance = CalculateDistanceToTarget(targetTransform);

                    if (targetHealth.IsAlive && distance >= _behaviorData._minDistance && distance <= _behaviorData._maxDistance)
                    {
                        _currentTargetTransform = targetTransform;
                        return 1;
                    }
                }
                return 0;
            }

            public override void Behave()
            {
                if (_navMeshAgent == null)
                {
                    return;
                }

                _navMeshAgent.isStopped = false;
                _navMeshAgent.speed = _behaviorData._speed;
                _navMeshAgent.SetDestination(_currentTargetTransform.position);
            }
        }
    }
}
