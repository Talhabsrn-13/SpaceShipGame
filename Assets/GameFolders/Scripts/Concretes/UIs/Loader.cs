using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Space.UIs
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] GameObject _loadScreen;
        public Slider _slider;
        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
            GameManager.Instance.BulletLvl = 1;
        }
        IEnumerator LoadAsynchronously(int sceneIndex)
        {
            _loadScreen.SetActive(true);
          
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);


            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                _slider.value = progress;
                yield return null;
            }
        }
    }

}
