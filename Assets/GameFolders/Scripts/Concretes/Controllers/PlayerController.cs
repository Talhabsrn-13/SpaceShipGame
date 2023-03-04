using Space.Abstract.Controller;
using Space.Abstract.Movements;
using Space.Movements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Space.Controller
{
    public class PlayerController : MonoBehaviour, IEntityController
    {
        [SerializeField] Transform bulletTransform;
        [SerializeField] GameObject bullet;
        [SerializeField] float _xRange;
        [SerializeField] float _yRange;
        Vector2 _destionation;
        IMover _mover;

        private void Awake()
        {
            _mover = new PlayerMovement(this);
            _destionation = transform.position;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(bullet,bulletTransform.transform.position, Quaternion.identity);
            }
            if (Input.GetMouseButton(0))
            {
                _destionation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                _destionation = new Vector2(transform.position.x, transform.position.y);
            }
        }
        private void FixedUpdate()
        {
            if (_destionation != null)
            {
                _mover.MoveAction(_destionation);
            }
        }
    }

}
