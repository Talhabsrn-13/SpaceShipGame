using Space.Abstract.Controller;
using Space.Managers;
using Space.Movements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Controller
{
    public class BulletController : MonoBehaviour , IEntityController
    {
        [SerializeField] float _verticalMoveSpeed;
        [SerializeField] float _maxLifeTime;
        
        VerticalMover _mover;
     
        float _currentLifeTime;
        public int Damage { get; set; }
        public object BUlletManager { get; private set; }

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
            Destroy(gameObject);
            BulletManager.Instance.SetPool(this);
            //EnemeyManager Instantiate
        }
        public void SetMoveSpeed(float moveSpeed = 10f)
        {
            if (moveSpeed < _verticalMoveSpeed) return;

            _verticalMoveSpeed = moveSpeed;
        }
    }
}
