using UnityEngine;

namespace Code.Shoot
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _movePower; 

        public void Move(Vector3 direction)
        {
            _rigidbody.AddForce(direction * _movePower);
        }
    }
}
