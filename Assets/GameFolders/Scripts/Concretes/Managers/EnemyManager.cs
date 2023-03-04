using Space.Abstract.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Managers
{
    public class EnemyManager : SingletonMonoBehaviourObject<EnemyManager>
    {
        private void Awake()
        {
            SingletonThisObject(this);
        }

    }

}
