using Space.Abstract.Controller;
using Space.Abstract.Entity;
using Space.Abstract.Movements;
using Space.Movements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Controller
{
    public class CollectableController : MonoBehaviour, IEntityController, ICollectable
    {
        [SerializeField] float _speed;
        [SerializeField] CollectableType _collectableType;
        VerticalMover _mover;
        float _currentTime=0;
        float _maxTime = 10f;
        public Transform Transform => this.transform;

        public CollectableType CollectableType => _collectableType;


        private void Awake()
        {
            _mover = new VerticalMover(this);
        }
        private void FixedUpdate()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >_maxTime)
            {
                _currentTime = 0;
                KillyourSelf();
            }
            _mover.FixedTick(false, _speed);
        }

        public void KillyourSelf()
        {
            Managers.CollectableManager.Instance.SetPool(this);
        }
    }
}
