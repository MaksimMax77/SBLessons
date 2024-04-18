using Code.Health;
using Code.Utils;
using Unity.Entities;
using UnityEngine;

namespace Code.AiUtilitySystem.Behaviors.AiUnitBehaviors
{
    [CreateAssetMenu(menuName = "Behaviors/" + nameof(AttackBehaviorData), fileName = nameof(AttackBehaviorData))]
    public class AttackBehaviorData : BehaviorData
    {
        [SerializeField] private float _attackDistance;
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _damage;
        
        public override IBehavior CreateBehavior(Entity aiAgentEntity)
        {
            return new AttackBehavior(this, aiAgentEntity);
        }
        
        private class AttackBehavior: AliveTargetOrientedBehavior<AttackBehaviorData>
        {
            private ObjectHealth _targetHealth;
            private Timer _timer;
            
            public AttackBehavior(AttackBehaviorData behaviorData, Entity aiAgentEntity) : base(behaviorData, aiAgentEntity)
            {
                _timer = new Timer();
                _timer.Init(_behaviorData._attackDelay);
            }

            public override float Evaluate()
            {
                for (int i = 0, len = _targets.Length; i < len; ++i)
                {
                    var targetEntity = _targets[i];
                    _targetHealth = _entityManager.GetComponentObject<ObjectHealth>(targetEntity);
                    var targetTransform = _entityManager.GetComponentObject<Transform>(targetEntity);

                    var distance = CalculateDistanceToTarget(targetTransform);

                    if (_targetHealth.IsAlive && distance <= _behaviorData._attackDistance)
                    {
                        return 1;
                    }
                }

                _targetHealth = null;
                return 0;
            }

            public override void Behave()
            {
                _navMeshAgent.isStopped = true;
                
                _timer.Update(Time.deltaTime);

                if (!_timer.IsEnd || _targetHealth == null)
                {
                    return;
                }
                _targetHealth.GetDamage(_behaviorData._damage);
                _timer.SetTimeZero();
            }
        }
    }
}
