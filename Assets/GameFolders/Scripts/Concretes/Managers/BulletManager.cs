using Space.Abstract.Utilities;
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
        private void Awake()
        {
            SingletonThisObject(this);
        }

        internal void SetPool(BulletController bulletController)
        {
            
        }
    }

}
