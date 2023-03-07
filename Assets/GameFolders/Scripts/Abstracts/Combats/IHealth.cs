using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Abstract.Combats
{
    public interface IHealth
    {
        bool IsDead { get; }
        void TakeDamage(int damage);
    }

}
