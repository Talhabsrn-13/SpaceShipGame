using Space.Abstract.Entity;
using Space.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Controller
{
    public class EffectController : MonoBehaviour
    {
        [SerializeField] BulletType _bulletType;
        [SerializeField] GameObject _parentTransform;
        public BulletType BulletType => _bulletType;
        private void Start()
        {
            transform.parent = _parentTransform.transform;
        }
       
        //Onenable.
        IEnumerator WaitforLose()
        {
            yield return new WaitForSeconds(1f);
            EffectManager.Instance.SetPool(this);
        }
    }
}
