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
        [SerializeField] EnemyType _enemyType;

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
            _chance = Random.Range(0, 3f);
            GetRandomSpawnTime();

        }
        private void Update()
        {
            if (!GameManager.Instance.Playability()) return;
            _currentTime += Time.deltaTime;
            if (_currentTime > _maxSpawnTime)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            EnemyBulletController newBullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            newBullet.transform.parent = BulletManager.Instance.transform;
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
            SoundManager.Instance.Play("EnemyExplosion2");
            GameManager.Instance.Score += 50;
            _eventData?.OnScore.Invoke();

            EffectController newEffect = EffectManager.Instance.GetPool(BulletType.EnemyExplosionEffect);
            newEffect.transform.position = transform.position;
            newEffect.gameObject.SetActive(true);

  
            if (_chance < 0.2f)
            {
                if (_chance <= 0.1f)
                {
                    _collectableType = 0; 
                }
                else
                {
                    _collectableType = 1;
                }
                CollectableController newItem = CollectableManager.Instance.GetPool((CollectableType)_collectableType);
                newItem.transform.parent = CollectableManager.Instance.transform;
                newItem.transform.position = new Vector3(transform.position.x, transform.position.y, 2);
                newItem.gameObject.SetActive(true);
            }
            EnemyManager.Instance.SetPool(this);
        }
    }
}
