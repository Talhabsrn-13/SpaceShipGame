using Space.Abstract.Combats;
using Space.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Combats
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] int _healthInfo;
  
        int _currentHealth;
        public bool IsDead => _currentHealth <= 0;
        private void Awake()
        {
            _currentHealth = _healthInfo;
        }
        public void TakeDamage(int damage)
        {
            if (IsDead) return;
            _currentHealth -= damage;
            if (IsDead)
            {
                GetComponent<EnemyController>().Death();
            }
        }
        private void OnEnable()
        {
            _currentHealth = 100;
        }
    }
}
