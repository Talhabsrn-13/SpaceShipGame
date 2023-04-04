using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Space.UIs;

namespace Space.Managers
{
    public class MapScreenManager : MonoBehaviour
    {
        EventData _eventData;

        [SerializeField] Image[] _planets;
        [SerializeField] Button _returnMenu;
        [SerializeField] Loader _loader;
        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
            _returnMenu.onClick.AddListener(ReturnMenu);
        }
        private void Start()
        {
            ColorControl();
            Debug.Log(GameManager.Instance.LastLevel);
        }
        public void ColorControl()
        {
            for (int i = 0; i < GameManager.Instance.LastLevel; i++)
            {
                if (_planets[i] != null)
                {
                    _planets[i].color = Color.white;
                }
            }

        }
        public void ButtonSound()
        {
            SoundManager.Instance.Play("Click");
        }
        private bool LevelControl(int index)
        {
            if (index <= GameManager.Instance.LastLevel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void LevelSelector(int index)
        {
            if (LevelControl(index))
            {
                GameManager.Instance.Level = index;
                _loader.LoadScene(1);
                _eventData?.OnPlay.Invoke();
            }
            else
            {
                SoundManager.Instance.Play("LevelNoHave");
            }
        }
        public void ReturnMenu()
        {
            _loader.LoadScene(0);
        }
        private void OnEnable()
        {
            _eventData.OnIdle += OnIdle;
        }
        private void OnDestroy()
        {

            _eventData.OnIdle -= OnIdle;

        }
        private void OnIdle()
        {
            ColorControl();
        }
    }

}
