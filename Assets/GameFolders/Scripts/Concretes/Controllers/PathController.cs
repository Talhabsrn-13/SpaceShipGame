using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathController : MonoBehaviour
{
    public Transform[] _points;

    [SerializeField] float moveSpeed;

    private int _pointsIndex;


    private void OnEnable()
    {
        transform.position = _points[_pointsIndex].transform.position;
    }

    private void Update()
    {
        if (_pointsIndex <= _points.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, _points[_pointsIndex].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == _points[_pointsIndex].transform.position)
            {
                _pointsIndex += 1;
            }

            if (_pointsIndex == _points.Length)
            {
                _pointsIndex = 1;
            }
        }
    }
}
