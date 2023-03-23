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

        [Header("Pause")]
        [SerializeField] Button _continueButton;
        [SerializeField] Button _pauseButton;
        [SerializeField] Button _pauseGoToMenuButton;

        [Header("LosePanel")]
        [SerializeField] GameObject _losePanel;
        [SerializeField] TMP_Text _loseScoreText;
        [SerializeField] TMP_Text _earnMoneyText;
        [SerializeField] Button _closeButton;
        [SerializeField] Button _repeatLvlButton;
        [SerializeField] Button _goToMapSceneButton;


        [Header("WinPanel")]
        [SerializeField] GameObject _winPanel;
        [SerializeField] TMP_Text _winScoreText;
        [SerializeField] TMP_Text _winEarnMoneyText;
        [SerializeField] Button _winMainMenuButton;
        [SerializeField] Button _winGoToMapButton;

        EventData _eventData;
        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
            _closeButton.onClick.AddListener(ReturnMainMenu);
            _repeatLvlButton.onClick.AddListener(RepeatLevel);
            _goToMapSceneButton.onClick.AddListener(GoToMapScene);
            _winMainMenuButton.onClick.AddListener(ReturnMainMenu);
            _winGoToMapButton.onClick.AddListener(GoToMapScene);
            _pauseButton.onClick.AddListener(PauseButton);
            _continueButton.onClick.AddListener(ContinueButton);
            _pauseGoToMenuButton.onClick.AddListener(ReturnMainMenu);
        }

        private void ContinueButton()
        {
            _eventData?.OnPlay.Invoke();
            Debug.Log(GameManager.Instance.GameState);
        }

        private void PauseButton()
        {
            _eventData?.OnIdle.Invoke(); 
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
            _winScoreText.text = GameManager.Instance.Score.ToString();
            _winEarnMoneyText.text = GameManager.Instance.EarnedMoneyData.ToString();
            _winPanel.SetActive(true);
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
