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
        public BulletType BulletType => _bulletType;

        private void Start()
        {
            transform.parent = GameObject.FindGameObjectWithTag("EffectParent").transform;
        }
        private void OnEnable()
        {
            Invoke("KillYourSelf", 0.2f);
            transform.parent = EffectManager.Instance.ParentTransform.transform;
        }
        public void KillYourSelf()
        {
            EffectManager.Instance.SetPool(this);
        }
    }
}
