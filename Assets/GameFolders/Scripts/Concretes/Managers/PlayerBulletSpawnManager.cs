using Space.Controller;
using Space.Enums;
using Space.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Managers
{
    public class PlayerBulletSpawnManager : MonoBehaviour
    {
        public bool CanShot { get; set; }
        [SerializeField] Transform _spawnerTransform;
        [SerializeField] float _fireTime;
        Vector2 _direction;
        PlayerController _player;
        //look
        private void Start()
        {
            _direction = (transform.localRotation * Vector2.up).normalized;
            InvokeRepeating("Spawn", 1, _fireTime);
            _player = GetComponentInParent<PlayerController>();
        }
        void Spawn()
        {
            if (CanShot) return;

            BulletController newBullet = BulletManager.Instance.GetPool((BulletType)_player.ShipType);
            newBullet.transform.parent = _spawnerTransform.transform;
            newBullet.transform.position = this.transform.position;
            newBullet.Direction = _direction;
            newBullet.transform.rotation = transform.rotation;
            newBullet.gameObject.SetActive(true);
        }

      
    }

}
