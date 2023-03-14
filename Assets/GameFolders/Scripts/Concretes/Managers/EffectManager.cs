using Space.Abstract.Entity;
using Space.Controller;
using Space.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonMonoBehaviourObject<EffectManager>
{
    [SerializeField] EffectController[] _effectPrefabs;

    Dictionary<BulletType, Queue<EffectController>> _effects = new Dictionary<BulletType, Queue<EffectController>>();
    private void Awake()
    {
        SingletonThisObject(this);
    }

    private void Start()
    {
        InitiliazePool();
    }

    private void InitiliazePool()
    {
        for (int i = 0; i < _effectPrefabs.Length; i++) 
        {
            Queue<EffectController> effectControllers = new Queue<EffectController>();
            for (int j = 0; j < 100; j++)
            {
                EffectController newEffect = Instantiate(_effectPrefabs[i]);
                newEffect.gameObject.SetActive(false);
                newEffect.transform.parent = this.transform;
                effectControllers.Enqueue(newEffect);
            }
            _effects.Add((BulletType)i, effectControllers);
        }
    }
    
    internal void SetPool(EffectController effectController)
    {
        effectController.gameObject.SetActive(false);
        effectController.transform.parent = this.transform;
        Queue<EffectController> effectControllers = _effects[effectController.BulletType];
        effectControllers.Enqueue(effectController);
    }
    public EffectController GetPool(BulletType bulletType)
    {
        Queue<EffectController> effectControllers = _effects[bulletType];
        if (effectControllers.Count < 100)
        {
            for (int i = 0; i < 10; i++)
            {
                EffectController newEffect = Instantiate(_effectPrefabs[(int)bulletType]);
                effectControllers.Enqueue(newEffect);
            }
        }

        return effectControllers.Dequeue();
    }
}
