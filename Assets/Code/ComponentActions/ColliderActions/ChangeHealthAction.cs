using Code.Health;
using UnityEngine;

namespace Code.ComponentActions.ColliderActions
{
    public class ChangeHealthAction : CollidersAction
    {
        [SerializeField] private bool _isDamageable; 
        [SerializeField] private float _changeValue;

        public override void Execute()
        {
            for (int i = 0, len = _collisions.Count; i < len; ++i)
            {
                var health = _collisions[i].GetComponent<ObjectHealth>();
                
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
