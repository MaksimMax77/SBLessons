using Code.Pool;
using Code.Projectiles;
using Code.Utils;
using UnityEngine;

namespace Code.ComponentActions.AbilityActions
{
    public class ShootAbility : ComponentAction
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _creationPoint;
        [SerializeField] private float _lifeTime;
        private ObjectsPool _objectsPool;

        public void SetPool(ObjectsPool pool)
        {
            _objectsPool = pool;
        }
        public override void Execute()
        {
            var projectile  = _objectsPool.GetOrCreateObject(_projectilePrefab);
            projectile.transform.position = _creationPoint.position;
            projectile.transform.rotation = Quaternion.identity;
            projectile.AddForce(transform.forward);
        }
    }
}
