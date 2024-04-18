namespace Code.Settings
{
    public class UserUnitSettingsDummy : IUserUnitSettings
    {
        private float _maxHealth = 0;
        private float _shootDelay = 0;
        private float _moveSpeed = 0;

        public float MaxHealth { get; }
        public float ShootDelay { get; }
        public float MoveSpeed { get; }
    }
}
