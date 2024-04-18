using System.Collections.Generic;
using Code.Collision.Actions;
using Code.Utils;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Collision
{
    public class CollisionControl : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private CollisionAction[] _actions;
        public List<Collider> Collisions { get; set; }
        public void OnCollisionCheck()
        {
            for (int i = 0, len = _actions.Length; i < len; ++i)
            {
                _actions[i].SetGameObject(gameObject);
                _actions[i].Execute(Collisions);
            }
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            float3 position = gameObject.transform.position;
            switch (_collider)
            {
                case SphereCollider sphere:
                    sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                    dstManager.AddComponentData(entity, new ActorColliderData
                    {
                        colliderType = ColliderType.Sphere,
                        sphereCenter = sphereCenter - position,
                        sphereRadius = sphereRadius,
                        initialTakeOff = true
                    });
                    break;

                case CapsuleCollider capsule:
                    capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd,
                        out var capsuleRadius);

                    dstManager.AddComponentData(entity, new ActorColliderData
                    {
                        colliderType = ColliderType.Capsule,
                        capsuleStart = capsuleStart - position,
                        capsuleEnd = capsuleEnd - position,
                        capsuleRadius = capsuleRadius,
                        initialTakeOff = false
                    });
                    break;

                case BoxCollider box:
                    box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientation);

                    dstManager.AddComponentData(entity, new ActorColliderData
                    {
                        colliderType = ColliderType.Box,
                        boxCenter = boxCenter - position,
                        boxHalfExtents = boxHalfExtents,
                        boxOrientation = boxOrientation,
                        initialTakeOff = true
                    });
                    break;
            }
        }
    }
    
    public struct ActorColliderData : IComponentData
    {
        public ColliderType colliderType;
        public float3 sphereCenter;
        public float sphereRadius;
        public float3 capsuleStart;
        public float3 capsuleEnd;
        public float capsuleRadius;
        public float3 boxCenter;
        public float3 boxHalfExtents;
        public quaternion boxOrientation;
        public bool initialTakeOff;
    }

    public enum ColliderType
    {
        Sphere,
        Capsule,
        Box
    }
}
