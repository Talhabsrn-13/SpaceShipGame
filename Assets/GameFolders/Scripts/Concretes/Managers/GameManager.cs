using Space.Abstract.Entity;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : SingletonMonoBehaviourObject<GameManager>
{

  
    EventData _eventData;
    GameState _gameState = GameState.Idle;

    int _gameScore;
    private int _bulletLvl = 1;
    public int Score
    {
        get
        {
            return _gameScore;
        }
        set
        {
            _gameScore = value;
        }
    }
    public int BulletLvl
    {
        get { return _bulletLvl; }
        set
        {
            _bulletLvl = value;
        }
    }
    private void Awake()
    {
        SingletonThisObject(this, true, false);
        _eventData = Resources.Load("EventData") as EventData;
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        LoadAudio();
    }


    public bool Playability()
    {
        return _gameState == GameState.Play;
    }
    public GameState GameState
    {
        get => _gameState;
        set => _gameState = value;
    }

    #region LastShip
    public int LastShipIndex
    {
        get => PlayerPrefs.GetInt("LastShipIndex", 0);
        set => PlayerPrefs.SetInt("LastShipIndex", value);
    }
    public int LastHaveShipIndex
    {
        get => PlayerPrefs.GetInt("LastHaveShipIndex", 0);
        set => PlayerPrefs.SetInt("LastHaveShipIndex", value);
    }
    #endregion

    #region MoneyData
    public int MoneyData
    {
        get => PlayerPrefs.GetInt("MoneyData", 0);
        set => PlayerPrefs.SetInt("MoneyData", value);
    }
    private int _earnedMoney;
    public int EarnedMoneyData
    {
        get { return _earnedMoney; }
        set { _earnedMoney = value; }
    }
    #endregion

    #region Level System
    public int WaveIndex { get; set; }
    public int MaxWaveCount { get; set; }

    public int LastLevel
    {
        get => PlayerPrefs.GetInt("LastLevel", 1);
        set => PlayerPrefs.SetInt("LastLevel", value);
    }
    public int Level
    {
        get => PlayerPrefs.GetInt("Level", 1);
        set => PlayerPrefs.SetInt("Level", value);
    }
    #endregion

    #region EventListener

    private void OnEnable()
    {
        _eventData.OnPlay += OnPlay;
        _eventData.OnWin += OnWin;
        _eventData.OnLose += OnLose;
        _eventData.OnIdle += OnIdle;

    }
    private void OnDestroy()
    {
        _eventData.OnPlay -= OnPlay;
        _eventData.OnWin -= OnWin;
        _eventData.OnLose -= OnLose;
        _eventData.OnIdle -= OnIdle;

    }
    private void OnPlay()
    {
        _gameState = GameState.Play;

        SoundManager.Instance.Stop("Menu");
        SoundManager.Instance.Play("Game");
    }
    private void OnWin()
    {
        _gameState = GameState.Win;
   
        WinLevel();
    }
    private void OnLose()
    {
        _gameState = GameState.Lose;
     
        LoseLevel();
    }
    private void OnIdle()
    {
        _gameState = GameState.Idle;
 
        SoundManager.Instance.Stop("Game");
        SoundManager.Instance.Play("Menu");
    }

    #endregion

    #region Audio
    public void SaveAudio()
    {
        PlayerPrefs.SetFloat("audioVolume", AudioListener.volume);
    }
    public void LoadAudio()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("audioVolume", 0.5f);
    }
    #endregion
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _eventData?.OnPlay.Invoke();
        BulletLvl = 1;
        Score = 0;
    }
    public void WinLevel()
    {
        SoundManager.Instance.Stop("Game");
        SoundManager.Instance.Play("Menu");
        SoundManager.Instance.Play("Win");

        EarnedMoneyData = Mathf.RoundToInt(Score / Random.Range(5, 10));
        MoneyData += EarnedMoneyData;
        if (LastLevel == Level)
        {
            if (LastLevel < 15)
            {
                LastLevel++;
            }
        }
    }
    public void LoseLevel()
    {
        SoundManager.Instance.Stop("Game");
        SoundManager.Instance.Play("Menu");
        SoundManager.Instance.Play("Lose");
        EarnedMoneyData = Mathf.RoundToInt(Score / Random.Range(10, 15));
        MoneyData += EarnedMoneyData;

        if (AdManager.Instance._interstitialAdLoaded)
        {
            AdManager.Instance.InterstitialAd.Show();
        }

    }
    public void NextLevel(int index)
    {
       
        StartCoroutine(LoadAsynchronously(index));
  
        _bulletLvl = 1;
    }
    public void StopGame()
    {
        Time.timeScale = 0f;
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

       
        while (!operation.isDone)
        {     
             yield return null;
        }
    }

}
