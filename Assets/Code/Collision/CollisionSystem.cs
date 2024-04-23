using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Collision
{
    public class CollisionSystem : ComponentSystem
    {
        private EntityQuery _collisionQuery;
        private Collider[] _results = new Collider[50];

        protected override void OnCreate()
        {
            _collisionQuery = GetEntityQuery(ComponentType.ReadOnly<ActorColliderData>(),
                ComponentType.ReadOnly<Transform>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_collisionQuery).ForEach(
                (Entity entity, Transform transform, ref ActorColliderData actorColliderData) =>
                {
                    var gameObject = transform.gameObject;
                    var position = (float3) gameObject.transform.position;
                    var rotation = gameObject.transform.rotation;
                    var collisionControl = transform.GetComponent<CollisionControl>();

                    if (collisionControl == null || !gameObject.activeInHierarchy)
                    {
                        return;
                    }

                    collisionControl.Colliders?.Clear();

                    int size = 0;

                    switch (actorColliderData.colliderType)
                    {
                        case ColliderType.Sphere:
                            size = Physics.OverlapSphereNonAlloc(actorColliderData.sphereCenter + position,
                                actorColliderData.sphereRadius, _results);
                            break;
                        case ColliderType.Capsule:
                            var center = ((actorColliderData.capsuleStart + position) +
                                          (actorColliderData.capsuleEnd + position)) / 2f;
                            var point1 = actorColliderData.capsuleStart + position;
                            var point2 = actorColliderData.capsuleEnd + position;
                            point1 = (float3) (rotation * (point1 - center)) + center;
                            point2 = (float3) (rotation * (point2 - center)) + center;
                            size = Physics.OverlapCapsuleNonAlloc(point1, point2,
                                actorColliderData.capsuleRadius, _results);
                            break;
                        case ColliderType.Box:
                            size = Physics.OverlapBoxNonAlloc(actorColliderData.boxCenter + position,
                                actorColliderData.boxHalfExtents, _results,
                                actorColliderData.boxOrientation * rotation);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (size > 0)
                    {
                        collisionControl.Colliders = GetColliders().ToList();
                        collisionControl.ExecuteActions();
                    }
                });
        }

        private IEnumerable<Collider> GetColliders()
        {
            var colliders = new List<Collider>();
            for (int i = 0, len = _results.Length; i < len; ++i)
            {
                if (_results[i] == null)
                {
                    continue;
                }
                colliders.Add(_results[i]);
            }
            return colliders;
        }
    }
}
