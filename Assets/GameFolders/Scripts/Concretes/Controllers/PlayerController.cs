using Space.Abstract.Controller;
using Space.Abstract.Movements;
using Space.Managers;
using Space.Movements;
using Space.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Space.Abstract.Entity;

namespace Space.Controller
{
    public class PlayerController : MonoBehaviour, IEntityController
    {
        [SerializeField] float _xRange;
        [SerializeField] float _yRange;
        [SerializeField] GameObject[] _gunPrefabs;
        Vector2 _destionation;
        IMover _mover;
        bool _isDead = false;
        [SerializeField] ShipType _shipType;
        public ShipType ShipType => _shipType;
        private void Awake()
        {
            _mover = new PlayerMovement(this);
            _destionation = transform.position;
        }
        private void Start()
        {

        }
        private void Update()
        {
            if (_isDead) return;
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
                //score alma level atlatma
                if (collectableController.CollectableType == CollectableType.GunPower)
                {
                    if (GameManager.Instance.BulletLvl > _gunPrefabs.Length)
                    {
                        GameManager.Instance.Score += 500;
                    }
                    else
                    {
                        GunLevelUp(GameManager.Instance.BulletLvl++);
                    }
                }
                other.GetComponent<CollectableController>().KillyourSelf();


                GameManager.Instance.Score += 500;

            }
        }
        private void GunLevelUp(int level)
        {   
            _gunPrefabs[level].SetActive(true);

            for (int i = 0; i < level; i++)
            {
                _gunPrefabs[i].GetComponent<PlayerBulletSpawnManager>().ResetInvoke();
            }
        }
        public void TakeDamage()
        {
            _isDead = true;
            GameManager.Instance.StopGame();
        }
    }
}
