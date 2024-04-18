using Unity.Entities;
using UnityEngine;

namespace Code.AiUtilitySystem.Behaviors
{
    public abstract class BehaviorData : ScriptableObject
    {
        public abstract IBehavior CreateBehavior(Entity aiAgentEntity);

        public abstract class Behavior<T> : IBehavior where T: BehaviorData
        {
            protected readonly T _behaviorData;
            protected Entity _aiAgentEntity;

            protected Behavior(T behaviorData, Entity aiAgentEntity)
            {
                _behaviorData = behaviorData;
                _aiAgentEntity = aiAgentEntity;
            }
            
            public abstract float Evaluate();
            public abstract void Behave();
        }
    }

    public interface IBehavior
    {
        public float Evaluate();
        public void Behave();
    }
}
