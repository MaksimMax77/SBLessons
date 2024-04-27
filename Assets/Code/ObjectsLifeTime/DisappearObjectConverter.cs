using Unity.Entities;
using UnityEngine;

namespace Code.ObjectsLifeTime
{
    public class DisappearObjectConverter : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private MonoBehaviour _disappearingObject; 
        [SerializeField] private float _lifeTime;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentObject(entity, new DisappearObjectData()
            {
                disappearingObject = _disappearingObject,
                lifeTime = _lifeTime,
            });
        }
    }

    public class DisappearObjectData : IComponentData
    {
        public MonoBehaviour disappearingObject;
        public float lifeTime;
        public bool alive;
    }
}
