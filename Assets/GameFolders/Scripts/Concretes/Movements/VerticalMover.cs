using Space.Abstract.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Movements
{
    public class VerticalMover : MonoBehaviour
    {
        IEntityController _entityController;
        public VerticalMover(IEntityController entityController)
        {
            _entityController = entityController;
        }

        public void FixedTick(bool direction, float moveSpeed)
        {
            if (direction)
            {
                _entityController.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            }
            else
            {
                _entityController.transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
            }
        }
    }

}
