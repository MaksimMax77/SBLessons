using UnityEngine;

namespace Code.Collisions
{
    public class CollisionCatcher : MonoBehaviour
    {
        private Collision _collision;

        public Collision GetCollision()
        {
            return _collision;
        }

        public void SetCollisionNull()
        {
            _collision = null;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            _collision = collision;
        }
    }
}
