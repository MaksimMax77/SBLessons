using System.Collections.Generic;
using Code.AiUtilitySystem.Behaviors;
using Unity.Entities;

namespace Code.AiUtilitySystem
{
    public class BehaviorsManager
    {
        private BehaviorData[] _behaviors;
        private bool _statesCreated;
        private List<IBehavior> _behaviorStates;
        private IBehavior _activeBehavior;

        public BehaviorsManager(BehaviorData[] behaviors)
        {
            _behaviors = behaviors;
        }
        
        public void Run(Entity behaviorEntity)
        {
            CreateBehaviorStates(behaviorEntity);
            ChooseBehavior();
            BehaveActiveBehavior();
        }
        
        private void CreateBehaviorStates(Entity behaviorEntity)
        {
            if (_statesCreated)
            {
                return;
            }
            
            _behaviorStates = new List<IBehavior>();

            for (int i = 0, len = _behaviors.Length; i < len; ++i)
            {
                _behaviorStates.Add(_behaviors[i].CreateBehavior(behaviorEntity));
            }

            _statesCreated = true;
        }

        private void ChooseBehavior()
        {
            var highScore = float.MinValue;

            for (int i = 0, len = _behaviorStates.Count; i < len; ++i)
            {
                var currentScores = _behaviorStates[i].Evaluate();

                if (currentScores > highScore)
                {
                    highScore = currentScores;
                    _activeBehavior = _behaviorStates[i];
                }
            }
        }

        private void BehaveActiveBehavior()
        {
            if (_activeBehavior == null)
            {
                return;
            }
            _activeBehavior.Behave();
        }
    }
}
