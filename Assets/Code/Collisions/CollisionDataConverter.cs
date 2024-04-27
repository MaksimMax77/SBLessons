using Unity.Entities;
using UnityEngine;

namespace Code.Collisions
{
    public class CollisionDataConverter : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private CollisionData _collisionData;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentObject(entity, _collisionData);
        }
    }
}
