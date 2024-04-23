using Code.Shoot;
using UnityEngine;

namespace Code.ComponentActions.AbilityActions
{
    public class ShootAbility : ComponentAction
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _creationPoint;

        public override void Execute()
        {
            var projectile = Instantiate(_projectilePrefab, _creationPoint.position, Quaternion.identity);
            projectile.Move(transform.forward);
        }
    }
}
