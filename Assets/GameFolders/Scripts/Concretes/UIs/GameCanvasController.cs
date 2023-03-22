using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Space.Abstract.Entity;
using Space.Controller;
using System;

namespace Space.UIs
{
    public class GameCanvasController : MonoBehaviour
    {
        [SerializeField] Image[] _bulletLevelImages;
        [SerializeField] TMP_Text _currentScore;
        [SerializeField] GameObject _losePanel;

        [Header("LosePanel")]
        [SerializeField] TMP_Text _loseScoreText;
        [SerializeField] TMP_Text _earnMoneyText;
        [SerializeField] Button _closeButton;
        [SerializeField] Button _repeatLvlButton;
        [SerializeField] Button _goToMapSceneButton;

        


        EventData _eventData;
        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
            _closeButton.onClick.AddListener(ReturnMainMenu);
            _repeatLvlButton.onClick.AddListener(RepeatLevel);
            _goToMapSceneButton.onClick.AddListener(GoToMapScene);
        }
        private void Start()
        {
                    }
        public void ReturnMainMenu()
        {
            SceneManager.LoadScene(0);
        }
        public void RepeatLevel()
        {
            GameManager.Instance.RestartGame();
            _losePanel.SetActive(false);
        }
        public void GoToMapScene()
        {
            //look
            Debug.Log("Go to Map Scene");
        }


        private void OnEnable()
        {
            _eventData.OnScore += SetScore;
            _eventData.OnBulletUpgrade += SetBulletLevel;
            _eventData.OnLose += OnLose;
            _eventData.OnWin += OnWin;
            _eventData.OnIdle += OnIdle;
            _eventData.OnPlay += OnPlay;
        }



        private void OnDisable()
        {
            _eventData.OnScore -= SetScore;
            _eventData.OnBulletUpgrade -= SetBulletLevel;
            _eventData.OnLose -= OnLose;
            _eventData.OnWin -= OnWin;
            _eventData.OnIdle -= OnIdle;
            _eventData.OnPlay -= OnPlay;
        }

        private void OnPlay()
        {
            //Play Screen.
            SetScore();
            SetBulletLevel();
        }

        private void OnIdle()
        {
            //pause Screen
           
        }
        private void OnWin()
        {
            
        }

        private void OnLose()
        {
            _loseScoreText.text = GameManager.Instance.Score.ToString();
            _earnMoneyText.text = GameManager.Instance.EarnedMoneyData.ToString();
            _losePanel.SetActive(true);
        }
        public void SetScore()
        {
            _currentScore.text = GameManager.Instance.Score.ToString();
        }
        public void SetBulletLevel()
        {
            for (int i = 0; i < _bulletLevelImages.Length; i++)
            {
                if (GameManager.Instance.BulletLvl > i)
                {
                    _bulletLevelImages[i].color = Color.white;
                }
                else
                {
                    _bulletLevelImages[i].color = new Color(1, 1, 1, 0.2f);
                }
              
            }
        }

    }

}
