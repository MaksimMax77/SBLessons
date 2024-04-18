using System.Collections.Generic;
using Code.Health;
using UnityEngine;

namespace Code.Collision.Actions
{
    [CreateAssetMenu(menuName = "CollisionActions/" + nameof(HeathChangeAction), fileName = nameof(HeathChangeAction))]
    public class HeathChangeAction : CollisionAction
    {
        [SerializeField] private bool _isDamageable; 
        [SerializeField] private float _changeValue;
        public override void Execute(List<Collider> collisions)
        {
            for (int i = 0, len = collisions.Count; i < len; ++i)
            {
                var health = collisions[i].GetComponent<ObjectHealth>();
                
                if (health == null)
                {
                    return;
                }

                if (_isDamageable)
                {
                     health.GetDamage(_changeValue);
                }
                else
                {
                    health.Add(_changeValue);
                }
            }
        }
    }
}
