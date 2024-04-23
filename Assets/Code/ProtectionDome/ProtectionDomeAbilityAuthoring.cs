using Code.ComponentActions.AbilityActions;
using Unity.Entities;
using UnityEngine;

namespace Code.ProtectionDome
{
    public class ProtectionDomeAbilityAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private ProtectionDomeAbility _ability;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentObject(entity, new ProtectionDomeData()
            {
                ability = _ability
            });
        }
    }

    public class ProtectionDomeData : IComponentData
    {
        public ProtectionDomeAbility ability;
    } 
}
