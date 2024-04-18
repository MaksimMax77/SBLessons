using UnityEngine;

namespace Code.Health
{
    public class ObjectHealth: MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        private float _currentHealth;
        public bool IsAlive => _currentHealth > 0;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void GetDamage(float damage)
        {
            _currentHealth -= damage;
            
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
            }
            
            Debug.LogError("Health = " + _currentHealth);
        }

        public void Add(float value)
        {
            _currentHealth += value;
            
            if (_currentHealth >= _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            
            Debug.LogError("Health = " + _currentHealth);
        }
    }
}