using Space.Abstract.Entity;
using Space.Controller;
using Space.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Managers
{
    public class EnemyManager : SingletonMonoBehaviourObject<EnemyManager>
    {
        [SerializeField] EnemyController[] _enemyPrefabs;

        Dictionary<EnemyType, Queue<EnemyController>> _enemies = new Dictionary<EnemyType, Queue<EnemyController>>();
        private void Awake()
        {
            SingletonThisObject(this, false,false);
        }

        private void Start()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            for (int i = 0; i < _enemyPrefabs.Length; i++)
            {
                Queue<EnemyController> enemyControllers = new Queue<EnemyController>();
                for (int j = 0; j < 100; j++)
                {
                    EnemyController newEnemy = Instantiate(_enemyPrefabs[i]);
                    newEnemy.gameObject.SetActive(false);
                    newEnemy.transform.parent = this.transform;
                    enemyControllers.Enqueue(newEnemy);
                }
                _enemies.Add((EnemyType)i, enemyControllers);
            }
        }

        internal void SetPool(EnemyController enemyController)
        {
            enemyController.gameObject.SetActive(false);
            enemyController.transform.parent = this.transform;
            Queue<EnemyController> enemyControllers = _enemies[enemyController.EnemyType];
            enemyControllers.Enqueue(enemyController);
        }

        public EnemyController GetPool(EnemyType enemyType)
        {
            Queue<EnemyController> enemyControllers = _enemies[enemyType];
            if (enemyControllers.Count < 20)
            {
                for (int i = 0; i < 10; i++)
                {
                    EnemyController newEnemy = Instantiate(_enemyPrefabs[(int)enemyType]);
                    newEnemy.gameObject.SetActive(false);
                    enemyControllers.Enqueue(newEnemy);
                }
            }

            return enemyControllers.Dequeue();
        }
    }

}
