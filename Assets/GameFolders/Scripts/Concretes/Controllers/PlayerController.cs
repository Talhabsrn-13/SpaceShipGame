using Space.Abstract.Controller;
using Space.Abstract.Movements;
using Space.Managers;
using Space.Movements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Space.Controller
{
    public class PlayerController : MonoBehaviour, IEntityController
    {
        [SerializeField] float _xRange;
        [SerializeField] float _yRange;
        Vector2 _destionation;
        IMover _mover;
        bool _isDead = false;
        private void Awake()
        {
            _mover = new PlayerMovement(this);
            _destionation = transform.position;
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
        }

        public void TakeDamage()
        {
            _isDead = true;
            GameManager.Instance.StopGame();
        }
    }
}
