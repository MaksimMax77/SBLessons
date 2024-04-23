using Code.Input;
using Unity.Entities;

namespace Code.ProtectionDome
{
    public class ProtectionDomeAbilitySystem : ComponentSystem
    {
        private EntityQuery _query;
    
        protected override void OnCreate()
        {
            _query = GetEntityQuery(ComponentType.ReadOnly<ProtectionDomeData>(), 
                ComponentType.ReadOnly<InputData>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_query).ForEach
            ((Entity entity, ProtectionDomeData protectionDomeData, ref InputData inputData) =>
                {
                    var ability = protectionDomeData.ability;
                    
                    if (inputData.isShieldAbilityButtonClick)
                    {
                        ability.RestartTimerIfIsEnd();
                    }
                    
                    ability.Execute();
                });
        }
    }
}
