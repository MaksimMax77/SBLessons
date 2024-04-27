using Code.ComponentActions;
using Code.ComponentActions.CollisionActions;
using Unity.Entities;

namespace Code.Collisions
{
    public class CollisionsSystem : ComponentSystem
    {
        private EntityQuery _collisionQuery;

        protected override void OnCreate()
        {
            _collisionQuery = GetEntityQuery(ComponentType.ReadOnly<CollisionData>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_collisionQuery).ForEach
            ((Entity entity, CollisionData collisionData) =>
            {
                var collisionCatcher = collisionData.collisionCatcher;
                var collision = collisionCatcher.GetCollision();
                var actions = collisionData.actionsManager.GetActions();

                if (collision == null)
                {
                    return;
                }

                SetCollisionToActions(collision, collisionData);
                ActionsExecute(actions);
                SetCollisionNull(collisionCatcher);
            });
        }

        private void SetCollisionToActions(UnityEngine.Collision collision, CollisionData collisionData)
        {
            var collisionActions = collisionData.actionsManager.GetActionsByType<CollisionAction>();

            if (collisionActions == null)
            {
                return;
            }

            for (int i = 0, len = collisionActions.Count; i < len; ++i)
            {
                collisionActions[i].SetCollision(collision);
            }
        }

        private void ActionsExecute(ComponentAction[] actions)
        {
            if (actions == null)
            {
                return;
            }
            
            for (int i = 0, len = actions.Length; i < len; ++i)
            {
                actions[i].Execute();
            }
        }

        private void SetCollisionNull(CollisionCatcher collisionCatcher)
        {
            collisionCatcher.SetCollisionNull();
        }
    }
}
