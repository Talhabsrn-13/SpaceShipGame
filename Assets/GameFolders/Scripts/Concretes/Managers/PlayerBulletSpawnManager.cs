using Space.Controller;
using Space.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Managers
{
    public class PlayerBulletSpawnManager : MonoBehaviour
    {
        public bool CanShot { get; set; }
        [SerializeField] Transform _playerTransform;
        [SerializeField] float _fireTime;
       
        //look
        private void Start()
        {
            InvokeRepeating("Spawn", 1, _fireTime);
        }
        void Spawn()
        {
            if (CanShot) return;

            BulletController newBullet = BulletManager.Instance.GetPool();
            newBullet.transform.parent = this.transform;
            newBullet.transform.position = _playerTransform.position;
            newBullet.gameObject.SetActive(true);
        }
    }

}
