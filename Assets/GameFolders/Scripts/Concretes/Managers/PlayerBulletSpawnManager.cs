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
        //look
        private void Start()
        {
            InvokeRepeating("Spawn", 1, 0.1f);
        }
        void Spawn()
        {
            if (CanShot) return;
            for (int i = 0; i < 2; i++)
            {
                BulletController newBullet = BulletManager.Instance.GetPool();
                newBullet.transform.parent = this.transform;
                newBullet.transform.position = _playerTransform.position;
                newBullet.gameObject.SetActive(true);
            }
           
        }
    }

}
