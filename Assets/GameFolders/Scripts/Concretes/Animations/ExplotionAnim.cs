using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Animations
{
    public class ExplotionAnim : MonoBehaviour
    {

        private void Start()
        {
            StartCoroutine(WaitforExplosion());
        }
 
        IEnumerator WaitforExplosion()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}

