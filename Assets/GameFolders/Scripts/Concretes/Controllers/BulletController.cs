using Space.Abstract.Combats;
using Space.Abstract.Controller;
using Space.Enums;
using Space.Managers;
using Space.Movements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Controller
{
    public class BulletController : MonoBehaviour , IEntityController
    {
        [SerializeField] BulletType _bulletType;
        [SerializeField] float _verticalMoveSpeed;
        [SerializeField] float _maxLifeTime;
        
        VerticalMover _mover;
        public BulletType BulletType => _bulletType;
        float _currentLifeTime;
        public int Damage { get; set; }
        public object BulletManager { get; private set; }

        private void Awake()
        {
            _mover = new VerticalMover(this);
        }
        private void Update()
        {
            _currentLifeTime += Time.deltaTime;
            if (_currentLifeTime > _maxLifeTime)
            {
                _currentLifeTime = 0;
                KillYourSelf();
            }
        }
        private void FixedUpdate()
        {
            _mover.FixedTick(true, _verticalMoveSpeed);
        }

        void KillYourSelf()
        {
            Managers.BulletManager.Instance.SetPool(this);
        }
        public void SetMoveSpeed(float moveSpeed = 10f)
        {
            if (moveSpeed < _verticalMoveSpeed) return;

            _verticalMoveSpeed = moveSpeed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IHealth>(out IHealth  health))
            {
                KillYourSelf();
                
                health.TakeDamage(10);
            }
        }
    }
}
