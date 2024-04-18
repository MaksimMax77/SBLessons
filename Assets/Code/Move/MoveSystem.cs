using Code.Input;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Move
{
    public class MoveSystem : ComponentSystem
    {
        private EntityQuery _query;
        protected override void OnCreate()
        {
            _query =  GetEntityQuery(ComponentType.ReadOnly<Transform>(), 
                ComponentType.ReadOnly<MoveData>());
        }
        protected override void OnUpdate()
        {
            Entities.With(_query).ForEach((Entity entity, ref MoveData moveData, Transform transform) =>
            {
                var inputData = EntityManager.GetComponentData<InputData>(entity);
                var speed = moveData.speed;
                var moveDirection = new Vector3(inputData.moveStickDirection.x, 0, inputData.moveStickDirection.y);
                
                if (moveDirection.Equals(float3.zero))
                {
                    return;
                }

                transform.position += moveDirection * speed * Time.DeltaTime;
                transform.rotation = GetMoveDirectionQuaternion(moveDirection);
            });
        }
        
        private quaternion GetMoveDirectionQuaternion(Vector3 moveDirection)
        {
            var lookRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            return new quaternion(lookRotation.x, lookRotation.y, lookRotation.z, lookRotation.w);
        }
    }
}
