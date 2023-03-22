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
        [SerializeField] Transform _spawnerTransform;
        [SerializeField] float _fireTime;
        Vector2 _direction;
        PlayerController _player;
      
        EventData _eventData;

        ParticleSystem _particle;

        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
           
        }
        private void Start()
        {
            _particle = GetComponentInChildren<ParticleSystem>();
            _direction = (transform.localRotation * Vector2.up).normalized;
            _player = GetComponentInParent<PlayerController>();

        }
        void Spawn()
        {
            if (!GameManager.Instance.Playability()) return;

            if (!_particle.isPlaying) _particle.Play();

            BulletController newBullet = BulletManager.Instance.GetPool((BulletType)_player.ShipType);
            newBullet.transform.parent = _spawnerTransform.transform;
            newBullet.transform.position = this.transform.position;
            newBullet.Direction = _direction;
            newBullet.transform.rotation = transform.rotation;
            newBullet.gameObject.SetActive(true);
        }
        public void ResetInvoke(bool CanShot)
        {

            CancelInvoke("Spawn");

            InvokeRepeating("Spawn", 0.1f, _fireTime);

        }


        #region EventListener

        private void OnEnable()
        {
            InvokeRepeating("Spawn", 1, _fireTime);
            _eventData.OnPlay += OnPlay;
            _eventData.OnWin += OnWin;
            _eventData.OnLose += OnLose;
            _eventData.OnIdle += OnIdle;
        }
        private void OnDisable()
        {
            CancelInvoke("Spawn");
            _eventData.OnPlay -= OnPlay;
            _eventData.OnWin -= OnWin;
            _eventData.OnLose -= OnLose;
            _eventData.OnIdle -= OnIdle;
        }
   
        private void OnPlay()
        {

        }
        private void OnWin()
        {
           

        }
        private void OnLose()
        {
        

        }
        private void OnIdle()
        {
           
        }

        #endregion

    }

}
