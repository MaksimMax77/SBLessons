using System.Collections.Generic;
using UnityEngine;

namespace Code.Collision.Actions
{
    public abstract class CollisionAction : ScriptableObject
    {
        protected GameObject _currenGameObject;
        public abstract void Execute(List<Collider> collisions);

        public void SetGameObject(GameObject gameObject)
        {
            _currenGameObject = gameObject;
        }
    }
}
