using Space.Abstract.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Controller
{
    public class EnemyController : MonoBehaviour, IEntityController, IEnemy
    {
        
        private void Start()
        {
            
        }
        public void Death()
        {
            Destroy(gameObject);
        }
    }
}
