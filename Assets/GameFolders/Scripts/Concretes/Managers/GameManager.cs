using Space.Abstract.Entity;
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

    private int _bulletLvl = 1;
    public int Score { get; set; }
    public int BulletLvl
    {
        get { return _bulletLvl; }
        set { _bulletLvl = value; }
    }
    private void Awake()
    {
        SingletonThisObject(this);
        _eventData = Resources.Load("EventData") as EventData;
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

    #region Level System
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
    }
    private void OnLose()
    {
        _gameState = GameState.Lose;
    }
    private void OnIdle()
    {
        _gameState = GameState.Idle;
    }

    #endregion

    private void Start()
    {
     //   StartCoroutine(WakeUp());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        //InGameGold = StartCountData;
        //_gameState = GameState.Idle;
        //LevelLastGold = 0;
        //EndlessLevel++;
        //Level++;
        //SceneManager.LoadScene(Level);
        ////StartCoroutine(StartAd());
    }

    public void StopGame()
    {
        Time.timeScale = 0f;
    }


}
