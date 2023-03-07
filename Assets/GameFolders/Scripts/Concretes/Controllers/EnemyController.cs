using Space.Abstract.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Controller
{
    public class EnemyController : MonoBehaviour, IEntityController, IEnemy
    {

        [SerializeField] GameObject _explosionEffect;
        private void Start()
        {

        }
        public void Death()
        {
            Instantiate(_explosionEffect, this.transform.position, Quaternion.identity);
            //pool
            Destroy(gameObject);
        }
        private void OnDisable()
        {
            //score++
        }
    }
}
