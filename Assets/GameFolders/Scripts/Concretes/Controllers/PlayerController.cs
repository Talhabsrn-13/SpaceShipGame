using Space.Abstract.Controller;
using Space.Abstract.Movements;
using Space.Managers;
using Space.Movements;
using Space.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Space.Abstract.Entity;
using Space.UIs;

namespace Space.Controller
{
    public class PlayerController : MonoBehaviour, IEntityController
    {
        [SerializeField] float _xRange;
        [SerializeField] float _yRange;
        [SerializeField] GameObject[] _gunPrefabs;
        [SerializeField] GameObject[] _ships;

       
        Vector2 _destionation;
        IMover _mover;

        bool _isDead = false;

        [SerializeField] ShipType _shipType;
        public ShipType ShipType => _shipType;

        EventData _eventData;
        private void Awake()
        {
            _mover = new PlayerMovement(this);
            _destionation = transform.position;
            _eventData = Resources.Load("EventData") as EventData;
            _eventData.Player = this;
        }
        private void Start()
        {
            _shipType = (ShipType)GameManager.Instance.LastHaveShipIndex;
            _ships[GameManager.Instance.LastHaveShipIndex].SetActive(true);

            for (int i = 0; i < _gunPrefabs.Length; i++)
            {
                _gunPrefabs[i] = _ships[GameManager.Instance.LastHaveShipIndex].GetComponent<ShipController>().Guns[i];
            }
        }
        private void Update()
        {
            if (_isDead || !GameManager.Instance.Playability()) return;
            if (Input.GetMouseButton(0))
            {
                _destionation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                _destionation = new Vector2(transform.position.x, transform.position.y);
            }
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -_xRange, _xRange), Mathf.Clamp(transform.position.y, -_yRange, _yRange), 0);
        }
        private void FixedUpdate()
        {
            if (_destionation != null)
            {
                _mover.MoveAction(_destionation);
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {

            IEnemy entityController = other.GetComponent<IEnemy>();
            if (entityController != null && !_isDead)
            {
                // Patlama efekti, oyundurmasý devam etmek istermisiniz demesi eklenecek.

                TakeDamage();
            }
            ICollectable collectableController = other.GetComponent<ICollectable>();
            if (collectableController != null && !_isDead)
            {
                if (collectableController.CollectableType == CollectableType.GunPower)
                {
                    GameManager.Instance.BulletLvl++;


                    if (GameManager.Instance.BulletLvl >= _gunPrefabs.Length)
                    {
                        GameManager.Instance.Score += 100;
                        _eventData?.OnScore.Invoke();
                    }
                    else
                    {
                        GunLevelUp();
                        _eventData?.OnBulletUpgrade.Invoke();
                    }
                }
                else if(collectableController.CollectableType == CollectableType.Score)
                {
                    GameManager.Instance.Score += 500;
                    _eventData?.OnScore.Invoke();
                }
                other.GetComponent<CollectableController>().KillyourSelf();
            }
        }
        private void GunLevelUp()
        {
            if (!GameManager.Instance.Playability()) return;

            if (GameManager.Instance.BulletLvl % 2 == 0)
            {
                _gunPrefabs[0].SetActive(false);
                _gunPrefabs[GameManager.Instance.BulletLvl].SetActive(true);
                _gunPrefabs[GameManager.Instance.BulletLvl - 1].SetActive(true);
            }
            else
            {
                _gunPrefabs[0].SetActive(true);
            }
            for (int i = 0; i < GameManager.Instance.BulletLvl - 1; i++)
            {
                if (GameManager.Instance.BulletLvl % 2 == 0 && i == 0) continue;


                _gunPrefabs[i].GetComponent<PlayerBulletSpawnManager>().ResetInvoke(false);
               
            }
        }
        public void TakeDamage()
        {
            _isDead = true;
            _eventData?.OnLose.Invoke();
        }
    }
}
