using Code.Health;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Code.AiUtilitySystem.Behaviors
{
    [CreateAssetMenu(menuName = "Behaviors/" + nameof(TrapRotationBehaviorData), fileName = nameof(TrapRotationBehaviorData))]
    public class TrapRotationBehaviorData : BehaviorData
    { 
        [SerializeField] private Vector3 _rotationDirection;
        [SerializeField] private float _rotationSpeed;
        
        public override IBehavior CreateBehavior(Entity aiAgentEntity)
        {
            return new TrapRotationBehavior(this, aiAgentEntity);
        }
        
        private class TrapRotationBehavior: Behavior<TrapRotationBehaviorData>
        {
            private Entity[] _targets;
            private EntityManager _entityManager;
            private Transform _behaviorEntityTransform;

            public TrapRotationBehavior(TrapRotationBehaviorData behaviorData, Entity aiAgentEntity) : base(behaviorData, aiAgentEntity)
            {
                _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                _behaviorEntityTransform = _entityManager.GetComponentObject<Transform>(_aiAgentEntity);
                CollectTargets();
            }

            public override float Evaluate()
            {
                var evaluateValue = 0f;
                
                for (int i = 0, len = _targets.Length; i < len; ++i)
                {
                    var targetEntity = _targets[i];
                    var targetHealth  = _entityManager.GetComponentObject<ObjectHealth>(targetEntity);
                    var targetTransform = _entityManager.GetComponentObject<Transform>(targetEntity);

                    if (!targetHealth.IsAlive)
                    {
                        continue;
                    }

                    evaluateValue = GetEvaluateValueByDistance(_behaviorEntityTransform, targetTransform, evaluateValue);
                }
                
                return evaluateValue;
            }

            public override void Behave()
            {
                _behaviorEntityTransform.Rotate(_behaviorData._rotationDirection
                                                * _behaviorData._rotationSpeed * Time.deltaTime);
            }

            private void CollectTargets()//todo дублирование
            {
                _targets = _entityManager
                    .CreateEntityQuery(ComponentType.ReadOnly<ObjectHealth>())
                    .ToEntityArray(Allocator.Temp).ToArray();
            }

            private float GetEvaluateValueByDistance(Transform behaviorTransform, Transform targetTransform, float oldValue)
            {
                var newEvaluateValue = 1/math.length(behaviorTransform.position - targetTransform.position);
                return newEvaluateValue > oldValue ? newEvaluateValue : oldValue;
            }
        }
    }
}
