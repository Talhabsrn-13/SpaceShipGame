using Space.Abstract.Entity;
using Space.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Managers
{
    public class CollectableManager : SingletonMonoBehaviourObject<CollectableManager>
    {
        [SerializeField] CollectableController[] _collectablePrefabs;
        Dictionary<CollectableType, Queue<CollectableController>> _collectables = new Dictionary<CollectableType, Queue<CollectableController>>();
        
        private void Awake()
        {
            SingletonThisObject(this,false,false);
        }
        private void Start()
        {
            InitiliazePool();
        }

        private void InitiliazePool()
        {
            for (int i = 0; i < _collectablePrefabs.Length; i++)
            {
                Queue<CollectableController> collectableControllers = new Queue<CollectableController>();
                for (int j = 0; j < 10; j++)
                {
                    CollectableController newItem = Instantiate(_collectablePrefabs[i]);
                    newItem.gameObject.SetActive(false);
                    newItem.transform.parent = this.transform;
                    collectableControllers.Enqueue(newItem);
                }
                _collectables.Add((CollectableType)i, collectableControllers);
            }
        }
        internal void SetPool(CollectableController collectController)
        {
            collectController.gameObject.SetActive(false);
            collectController.transform.parent = this.transform;
            Queue<CollectableController> collectableControllers = _collectables[collectController.CollectableType];
            collectableControllers.Enqueue(collectController);
        }
        public CollectableController GetPool(CollectableType collectableType)
        {
            Queue<CollectableController> collectControllers = _collectables[collectableType];
            if (collectControllers.Count < 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    CollectableController newItem = Instantiate(_collectablePrefabs[(int)collectableType]);
                    collectControllers.Enqueue(newItem);
                }
            }
            return collectControllers.Dequeue();
        }

    }
}

