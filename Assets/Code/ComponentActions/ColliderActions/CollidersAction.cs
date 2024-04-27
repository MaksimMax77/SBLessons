using System.Collections.Generic;
using UnityEngine;

namespace Code.ComponentActions.ColliderActions
{
    public abstract class CollidersAction : ComponentAction
    {
        protected List<Collider> _collisions;
        public void SetColliders(List<Collider> collisions)
        {
            _collisions = collisions;
        }
    }
}
