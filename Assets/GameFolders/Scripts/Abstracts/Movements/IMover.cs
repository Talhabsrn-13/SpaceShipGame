using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Abstract.Movements
{
    public interface IMover
    {
        void MoveAction(Vector2 destination);
    }

}
