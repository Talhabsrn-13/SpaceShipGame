using Space.Controller;
using Space.Enums;
using Space.Managers;
using Space.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }
    EventData _eventData;

    [Header("Paths")]
    [SerializeField] Transform[] _path1;
    [SerializeField] Transform[] _path2;
    [SerializeField] Transform[] _path3;
    [SerializeField] Transform[] _path4;
    [SerializeField] Transform[] _path5;
    [SerializeField] Transform[] _path6;
    [SerializeField] Transform[] _path7;
    [SerializeField] Transform[] _path8;
    [SerializeField] Transform[] _path9;
    [SerializeField] Transform[] _path10;
    Transform[][] _paths;
    [Header("TargetTransforms")]
    [SerializeField] Transform[] _targetTransform1;
    [SerializeField] Transform[] _targetTransform2;
    [SerializeField] Transform[] _targetTransform3;
    [SerializeField] Transform[] _targetTransform4;
    [SerializeField] Transform[] _targetTransform5;

    Transform[][] _targets;
    [SerializeField] WaveSO[] _waveSO;
    [System.Serializable]

    public class Wave
    {
        public int pathType;
        public int enemyType;
        public int count;
        public float rate;
        public int targetType;
    }

    private Wave[] waves;
    private int nextWave = 0;
    static bool _leftOrRight;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    private void Awake()
    {
        _eventData = Resources.Load("EventData") as EventData;
    }
    private void Start()
    {
        waveCountdown = timeBetweenWaves;
       
        waves = _waveSO[GameManager.Instance.Level-1]._waves;
       
        WaveInformation();
        _targets = new Transform[][]
        {
            _targetTransform1,
            _targetTransform2,
            _targetTransform3,
            _targetTransform4,
            _targetTransform5

        };
        _paths = new Transform[][]
         {
          _path1,
          _path2,
          _path3,
          _path4,
          _path5,
          _path6,
          _path7,
          _path8,
          _path9,
          _path10
        };
    }
    private void Update()
    {
        if (!GameManager.Instance.Playability()) return;
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }
    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            _eventData?.OnWin.Invoke();
        }
        else
        {
            nextWave++;
            WaveInformation(); 
        }
    }
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }

        }
        return true;
    }
    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            if (!GameManager.Instance.Playability()) continue;
            SpawnEnemy(_wave.enemyType, _wave.pathType, _wave.targetType, i % _targets[_wave.targetType].Length);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(int _enemyType, int _pathType,int _targetType, int _targetTransformIndex)
    {
        if (_leftOrRight)
        {

            EnemyController newEnemy = EnemyManager.Instance.GetPool((EnemyType)_enemyType);
            newEnemy.transform.parent = this.transform;
            newEnemy.transform.position = transform.position;
            newEnemy.transform.rotation = transform.rotation;
            for (int i = 0; i < _path1.Length; i++)
            {
                newEnemy.GetComponent<PathController>()._points[i] = _paths[_pathType][i];
            }
            newEnemy.GetComponent<PathController>()._targetTransform =  _targets[_targetType][_targetTransformIndex];
            newEnemy.gameObject.SetActive(true);
            _leftOrRight = !_leftOrRight;
        }
        else
        {

            EnemyController newEnemy = EnemyManager.Instance.GetPool((EnemyType)_enemyType);
            newEnemy.transform.parent = this.transform;
            newEnemy.transform.position = transform.position;
            newEnemy.transform.rotation = transform.rotation;
            for (int i = 0; i < _path1.Length; i++)
            {
                newEnemy.GetComponent<PathController>()._points[i] = _paths[_pathType+1][i];
            }
            newEnemy.GetComponent<PathController>()._targetTransform = _targets[_targetType][_targetTransformIndex];
            newEnemy.gameObject.SetActive(true);
            _leftOrRight = !_leftOrRight;
        }
    }
    private void WaveInformation()
    {
        GameManager.Instance.MaxWaveCount = waves.Length;
        GameManager.Instance.WaveIndex = nextWave;
        _eventData?.OnWaveComplated.Invoke();
    }

    #region EventData


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

    }
    private void OnWin()
    {
        waveCountdown = timeBetweenWaves;
        nextWave = 0;

    }
    private void OnLose()
    {
        waveCountdown = timeBetweenWaves;
        nextWave = 0;

    }
    private void OnIdle()
    {
        waveCountdown = timeBetweenWaves;
    }
    #endregion
}
