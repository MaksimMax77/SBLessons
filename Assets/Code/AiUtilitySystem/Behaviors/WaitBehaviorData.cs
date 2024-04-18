using Unity.Entities;
using UnityEngine;

namespace Code.AiUtilitySystem.Behaviors
{
    [CreateAssetMenu(menuName = "Behaviors/" + nameof(WaitBehaviorData), fileName = nameof(WaitBehaviorData))]
    public class WaitBehaviorData : BehaviorData
    {
        public override IBehavior CreateBehavior(Entity aiAgentEntity)
        {
            return new WaitBehavior(this, aiAgentEntity);
        }

        private class WaitBehavior : Behavior<WaitBehaviorData>
        {
            public WaitBehavior(WaitBehaviorData behaviorData, Entity aiAgentEntity) : base(behaviorData, aiAgentEntity)
            {
            }

            public override float Evaluate()
            {
                return 0.5f;
            }

            public override void Behave()
            {
            }
        }
    }
}
