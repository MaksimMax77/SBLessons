using Code.ComponentActions;
using Unity.Entities;
using UnityEngine;

namespace Code.Collisions
{
    public class CollisionData : MonoBehaviour, IComponentData
    {
        public CollisionCatcher collisionCatcher;
        public ActionsManager actionsManager;
    }
}