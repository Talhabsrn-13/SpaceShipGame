using Space.Abstract.Entity;
using Space.Controller;
using Space.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Managers
{
    public class BulletManager : SingletonMonoBehaviourObject<BulletManager>
    {
        [SerializeField] BulletController[] _bulletPrefabs;

        Dictionary<BulletType, Queue<BulletController>> _bullets = new Dictionary<BulletType, Queue<BulletController>>();
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
                Queue<BulletController> bulletControllers = new Queue<BulletController>();
                for (int j = 0; j < 3000; j++)
                {
                    BulletController newBullet = Instantiate(_bulletPrefabs[i]);
                    newBullet.gameObject.SetActive(false);
                    newBullet.transform.parent = this.transform;
                    bulletControllers.Enqueue(newBullet);
                }
                _bullets.Add((BulletType)i, bulletControllers);
            }
        }

        internal void SetPool(BulletController bulletController)
        {
            bulletController.gameObject.SetActive(false);
            bulletController.transform.parent = this.transform;
            Queue<BulletController> bulletControllers = _bullets[bulletController.BulletType];
            bulletControllers.Enqueue(bulletController);
        }
        public BulletController GetPool(BulletType bulletType)
        {
            Queue<BulletController> bulletControllers = _bullets[bulletType];
            if (bulletControllers.Count < 10)
            {
                for (int i = 0; i < 2; i++)
                {
                    BulletController newBullet = Instantiate(_bulletPrefabs[(int)bulletType]);
                    bulletControllers.Enqueue(newBullet);
                }
            }

            return bulletControllers.Dequeue();
        }
    }

}
