using Code.AiUtilitySystem.Behaviors;
using Unity.Entities;
using UnityEngine;

namespace Code.AiUtilitySystem
{
    public class AiAgentAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
       [SerializeField] private BehaviorData[] _behaviors;
       public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentObject(entity, new AiAgentData()
            {
                behaviorsManager = new BehaviorsManager(_behaviors)
            });
        }
    }

    public class AiAgentData : IComponentData
    {
        public BehaviorsManager behaviorsManager;
    }
}
