using Space.Abstract.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Controller
{
    public class EnemyController : MonoBehaviour, IEntityController, IEnemy
    {

        [SerializeField] GameObject _explosionEffect;
        [Range(10, 15)] [SerializeField] float _min;
        [Range(15, 30)] [SerializeField] float _max;

        float _maxSpawnTime;
        float _currentTime = 0f;
        bool _canIncrease = true;
        [SerializeField] EnemyBulletController _bullet;
        public bool CanIncrease => _canIncrease;
        private void Start()
        {

        }
       

        private void OnEnable()
        {
            GetRandomSpawnTime();
        }
        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > _maxSpawnTime)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            EnemyBulletController newBullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            newBullet.transform.parent = this.transform;
            newBullet.transform.position = this.transform.position;
            newBullet.gameObject.SetActive(true);

            _currentTime = 0;
            GetRandomSpawnTime();

        }

        private void GetRandomSpawnTime()
        {
            _maxSpawnTime = Random.Range(_min, _max);
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
