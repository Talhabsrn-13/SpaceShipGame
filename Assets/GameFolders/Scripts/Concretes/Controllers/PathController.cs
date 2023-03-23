using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathController : MonoBehaviour
{
    public Transform[] _points;

   public Transform _targetTransform;
    [SerializeField] float moveSpeed;

    private int _pointsIndex;

    bool _isComplate;
    private void OnEnable()
    {
        _isComplate = false;
        transform.position = _points[0].transform.position;
    }

    private void FixedUpdate()
    {

        if (_isComplate)
        {
            OnTarget();
        }
        else
        {
            Move();
        }
       
    }
    private void Move()
    {
        if (_pointsIndex <= _points.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, _points[_pointsIndex].transform.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (transform.position == _points[_pointsIndex].transform.position)
            {
                _pointsIndex += 1;
            }

            if (_pointsIndex == _points.Length)
            {
                _isComplate = true;
                _pointsIndex = 1;
            }
        }
    }
    private void OnTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetTransform.transform.position, moveSpeed * Time.deltaTime);
    }
}
