using System;
using UnityEngine;

namespace Code.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _movePower;

        public void AddForce(Vector3 direction)
        {
            _rigidbody.AddForce(direction * _movePower);
        }
        
        private void OnDisable()
        {
            _rigidbody.isKinematic = true;
        }
    }
}
