using Space.Abstract.Controller;
using Space.Abstract.Movements;
using Space.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Movements
{
    public class PlayerMovement :  IMover
    {
        private IEntityController _entity;
        private Vector2 _destinationPosition;

        public PlayerMovement(PlayerController entity)
        {
            _entity = entity;
        }
      
        public void MoveAction(Vector2 destination)
        {
            if (destination == new Vector2(_entity.transform.position.x, _entity.transform.position.y)) return;

            _destinationPosition = destination;
            _entity.transform.position = new Vector3(Mathf.Lerp(_entity.transform.position.x, _destinationPosition.x, 0.1f), Mathf.Lerp(_entity.transform.position.y, _destinationPosition.y, 0.1f), 0);
        }
    }
}

