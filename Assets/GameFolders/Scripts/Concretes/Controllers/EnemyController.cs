using Space.Abstract.Controller;
using Space.Enums;
using Space.Managers;
using Space.UIs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Controller
{
    public class EnemyController : MonoBehaviour, IEntityController, IEnemy
    {


        [Range(10, 15)] [SerializeField] float _min;
        [Range(15, 30)] [SerializeField] float _max;

        float _maxSpawnTime;
        float _currentTime = 0f;
        bool _canIncrease = true;
        float _chance;
        int _collectableType;
        EnemyType _enemyType;

        public EnemyType EnemyType => _enemyType;


        EventData _eventData;

        [SerializeField] EnemyBulletController _bullet;
        public bool CanIncrease => _canIncrease;
        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            _chance = Random.Range(0, 2f);
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
            GameManager.Instance.Score += 50;
            _eventData?.OnScore.Invoke();

            EffectController newEffect = EffectManager.Instance.GetPool(BulletType.EnemyExplosionEffect);
            newEffect.transform.position = transform.position;
            newEffect.gameObject.SetActive(true);

  
            if (_chance < 0.3f)
            {
                if (_chance <= 0.1f)
                {
                    _collectableType = 1; 
                }
                else
                {
                    _collectableType = 0;
                }
                CollectableController newItem = CollectableManager.Instance.GetPool((CollectableType)_collectableType);
                newItem.transform.parent = CollectableManager.Instance.transform;
                newItem.transform.position = new Vector3(transform.position.x, transform.position.y, 2);
                newItem.gameObject.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
