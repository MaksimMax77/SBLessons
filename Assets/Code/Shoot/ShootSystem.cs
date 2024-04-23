using Code.ComponentActions.AbilityActions;
using Code.Input;
using Code.Utils;
using Unity.Entities;

namespace Code.Shoot
{
    public class ShootSystem : ComponentSystem
    {
        private EntityQuery _query;
        private Timer _timer;

        protected override void OnCreate()
        {
            _query = GetEntityQuery(ComponentType.ReadOnly<ShootData>(), 
                ComponentType.ReadOnly<InputData>());
            _timer = new Timer();
        }

        protected override void OnUpdate()
        {
            Entities.With(_query).ForEach((Entity entity, ref ShootData shootData, ref InputData inputData) =>
            {
                _timer.Init(shootData.shootDelay);

                if (!inputData.isShootButtonClick)
                {
                    _timer.SetMaxValue();
                    return;
                }

                _timer.Update(Time.DeltaTime);

                if (!_timer.IsEnd)
                {
                    return;
                }

                var command = EntityManager.GetComponentObject<ShootAbility>(entity);
                command.Execute();
                _timer.Restart();
            });
        }
    }
}
