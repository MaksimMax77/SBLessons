using Unity.Entities;
using UnityEngine;

namespace Code.AiUtilitySystem
{
    public class AiBehaveSystem : ComponentSystem
    {
        private EntityQuery _aiAgentQuery;

        protected override void OnCreate()
        {
            _aiAgentQuery = GetEntityQuery(ComponentType.ReadOnly<AiAgentData>(),
                ComponentType.ReadOnly<Transform>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_aiAgentQuery).ForEach((Entity entity, AiAgentData aiAgentData, Transform transform) =>
            {
                aiAgentData.behaviorsManager.Run(entity);
            });
        }
    }
}
