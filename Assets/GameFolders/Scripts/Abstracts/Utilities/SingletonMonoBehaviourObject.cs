using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Space.Abstract.Entity
{
    public class SingletonMonoBehaviourObject<T> : MonoBehaviour
    {
        public static T Instance { get; private set; }

        public bool isActiveInGameScene;
        private bool subscribed = true;

        protected void SingletonThisObject(T entity, bool isEveryScene)
        {
            isActiveInGameScene = isEveryScene;
            if (Instance == null)
            {
                Instance = entity;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void OnEnable()
        {
            if (subscribed)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
        }
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            subscribed = false;
            if (isActiveInGameScene)
            {
                gameObject.SetActive(true);
            }
            else if (scene.buildIndex != 0)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

}
