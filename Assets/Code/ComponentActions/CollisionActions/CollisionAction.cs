using System.Collections.Generic;
using UnityEngine;

namespace Code.ComponentActions.CollisionActions
{
    public abstract class CollisionAction : ComponentAction
    {
        protected List<Collider> _collisions;
        public void SetColliders(List<Collider> collisions)
        {
            _collisions = collisions;
        }
    }
}
