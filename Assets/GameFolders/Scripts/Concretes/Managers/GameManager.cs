using Space.Abstract.Entity;
using Space.UIs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : SingletonMonoBehaviourObject<GameManager>
{
    [SerializeField] int levelCount;
    [SerializeField] int randomLevelLowerLimit;

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
        SingletonThisObject(this, true);
        _eventData = Resources.Load("EventData") as EventData;
    }
    private void Start()
    {
        //   StartCoroutine(WakeUp());
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
        get => PlayerPrefs.GetInt("MoneyData", 10000);
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
    public int EndlessLevel
    {
        get => PlayerPrefs.GetInt("EndlessLevel", 1);
        set => PlayerPrefs.SetInt("EndlessLevel", value);
    }
    public int Level
    {
        get
        {
            if (PlayerPrefs.GetInt("Level") > levelCount)
            {
                return Random.Range(randomLevelLowerLimit, levelCount);
            }
            else
            {
                return PlayerPrefs.GetInt("Level", 1);
            }
        }
        set => PlayerPrefs.SetInt("Level", EndlessLevel);
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

    }
    private void OnWin()
    {
        _gameState = GameState.Win;
        EarnedMoneyData = Mathf.RoundToInt(Score / Random.Range(5, 10));
        MoneyData += EarnedMoneyData;
    }
    private void OnLose()
    {
        _gameState = GameState.Lose;
        EarnedMoneyData = Mathf.RoundToInt(Score / Random.Range(10, 15));
        MoneyData += EarnedMoneyData;
    }
    private void OnIdle()
    {
        _gameState = GameState.Idle;
    }

    #endregion


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _eventData?.OnPlay.Invoke();
        BulletLvl = 1;
        Score = 0;
    }
    public void NextLevel(int index)
    {
        SceneManager.LoadScene(index);
        _bulletLvl = 1;
        //InGameGold = StartCountData;
        //_gameState = GameState.Idle;
        //LevelLastGold = 0;
        //EndlessLevel++;
        //Level++;
        //SceneManager.LoadScene(Level);
        ////StartCoroutine(StartAd());
    }
    public void LoseLevel()
    {
     
    }
    public void StopGame()
    {
        Time.timeScale = 0f;
    }


}
