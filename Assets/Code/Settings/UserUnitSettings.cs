using UnityEngine;

namespace Code.Settings
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(UserUnitSettings))]
    public class UserUnitSettings : ScriptableObject, IUserUnitSettings
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _shootDelay;
        [SerializeField] private float _moveSpeed;

        public float MaxHealth => _maxHealth;
        public float ShootDelay => _shootDelay;
        public float MoveSpeed => _moveSpeed;
    }
}
