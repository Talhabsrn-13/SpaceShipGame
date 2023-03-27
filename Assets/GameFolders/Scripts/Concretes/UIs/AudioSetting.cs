using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Space.UIs
{
    public class AudioSetting : MonoBehaviour
    {
        [SerializeField] Slider _slider;
        private void Start()
        {
            LoadAudio();
        }
        public void SetAudio()
        {
           
            AudioListener.volume = _slider.value;
            GameManager.Instance.SaveAudio();
        }
        
        private void LoadAudio()
        {
            AudioListener.volume = PlayerPrefs.GetFloat("audioVolume", 0.5f);
            _slider.value = PlayerPrefs.GetFloat("audioVolume", 0.5f);
        }
    }
}
