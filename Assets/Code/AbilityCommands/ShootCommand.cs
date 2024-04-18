using Code.Shoot;
using UnityEngine;

namespace Code.AbilityCommands
{
    public class ShootCommand : AbilityCommand
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
