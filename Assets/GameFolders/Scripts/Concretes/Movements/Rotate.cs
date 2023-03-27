using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Movements
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] float _turnSpeed;
        float _maxTime = 3f;
        float _possitiveValue = 1;
        private void Update()
        {
            _maxTime -= Time.deltaTime;
            if (_maxTime < 0)
            {
                _maxTime = 3f;
                _possitiveValue = -_possitiveValue;
            }
            transform.Rotate(Vector3.forward, Time.deltaTime * _turnSpeed * _possitiveValue);
        }

    }

}
