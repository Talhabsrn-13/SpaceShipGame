using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTransformMovement : MonoBehaviour
{
    float _maxTime = 1f;
    float _possitiveValue = 1;
    [SerializeField] float _onTargetSpeed =1.5f;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.Playability()) return;
        _maxTime -= Time.deltaTime;
        if (_maxTime < 0)
        {
            _maxTime = 1f;
            _possitiveValue = -_possitiveValue;
        }
        transform.Translate(Vector3.right * _onTargetSpeed * Time.deltaTime * _possitiveValue);
       
    }
}
