using Space.Abstract.Entity;
using Space.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Managers
{
    public class BulletManager : SingletonMonoBehaviourObject<BulletManager>
    {
        [SerializeField] BulletController[] _bulletPrefabs;

        Queue<BulletController> _bullets = new Queue<BulletController>();
        private void Awake()
        {
            SingletonThisObject(this);
        }

        private void Start()
        {
            InitiliazePool();
        }

        private void InitiliazePool()
        {

            for (int i = 0; i < _bulletPrefabs.Length; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    BulletController newBullet = Instantiate(_bulletPrefabs[i]);
                    newBullet.gameObject.SetActive(false);
                    newBullet.transform.parent = this.transform;
                    _bullets.Enqueue(newBullet);
                }
            }
        }

        internal void SetPool(BulletController bulletController)
        {
            bulletController.gameObject.SetActive(false);
            bulletController.transform.parent = this.transform;
            _bullets.Enqueue(bulletController);
        }
        public BulletController GetPool()
        {
            if (_bullets.Count == 0)
            {
                InitiliazePool();
            }
            return _bullets.Dequeue();
        }
    }

}
