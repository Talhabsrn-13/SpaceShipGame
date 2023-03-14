using Space.Abstract.Controller;
using Space.Managers;
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
        [SerializeField] Transform _spawnerTransform;
        float _maxSpawnTime;
        float _currentTime = 0f;
        bool _canIncrease = true;
        float _chance;



        [SerializeField] EnemyBulletController _bullet;
        public bool CanIncrease => _canIncrease;
        private void Start()
        {
            _chance = Random.Range(0, 1f);

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
            if (_chance < 0.1f)
            {
                CollectableController newItem = CollectableManager.Instance.GetPool((CollectableType)0);
                newItem.transform.parent = _spawnerTransform.transform;
                newItem.transform.position = new Vector3(transform.position.x, transform.position.y, 2);
                newItem.gameObject.SetActive(true);
            }
            Destroy(gameObject);
        }
        private void OnDisable()
        {
            GameManager.Instance.Score += 100;
        }
    }
}
