using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Abstract.Entity
{
    public interface ICollectable
    {
        public Transform Transform { get; }
        CollectableType CollectableType { get; }
    }

}
