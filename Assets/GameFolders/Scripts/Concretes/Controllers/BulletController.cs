using Space.Abstract.Combats;
using Space.Abstract.Controller;
using Space.Enums;
using Space.Managers;
using Space.Movements;
using Space.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Controller
{
    public class BulletController : MonoBehaviour, IEntityController
    {
        [SerializeField] BulletType _bulletType;
        private float _maxLifeTime = 2f;

        [SerializeField] Vector2 _direction = new Vector2(0, 1);
        [SerializeField] float _speed = 2f;
        [SerializeField] Vector2 _velocity;

        [SerializeField] ShopItemSO _bulletTypeSO;
        public BulletType BulletType => _bulletType;
        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        float _currentLifeTime;
        public int Damage { get; set; }
        public object BulletManager { get; private set; }


        private void Update()
        {
            _velocity = _direction * _speed;
            _currentLifeTime += Time.deltaTime;
            if (_currentLifeTime > _maxLifeTime)
            {
                _currentLifeTime = 0;
                KillYourSelf();
            }
        }

        private void FixedUpdate()
        {
            Vector2 pos = transform.position;
            pos += _velocity * Time.fixedDeltaTime;

            transform.position = pos;
        }

        void KillYourSelf()
        {
            Managers.BulletManager.Instance.SetPool(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IHealth>(out IHealth health))
            {

                EffectController newEffect = EffectManager.Instance.GetPool((BulletType)_bulletType);
                newEffect.transform.position = transform.position;
                newEffect.gameObject.SetActive(true);

                KillYourSelf();
                health.TakeDamage(_bulletTypeSO.damage);
                Debug.Log(_bulletTypeSO.damage);
            }
        }
    }
}
