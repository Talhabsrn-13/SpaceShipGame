using Space.Controller;
using Space.Enums;
using Space.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }
    EventData _eventData;
    [SerializeField] Transform[] _paths;
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;
    bool rightOrLeft;

    [SerializeField] Transform[] spawnPoints;

    Transform _sp;
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
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
    
        if (rightOrLeft)
        {
            _sp = spawnPoints[0];
            rightOrLeft = false;
        }
        else
        {
            _sp = spawnPoints[1];
            rightOrLeft = true;
        }
       
        EnemyController newEnemy = EnemyManager.Instance.GetPool((EnemyType)0);
        newEnemy.transform.parent = _sp.transform;
        newEnemy.transform.position = _sp.transform.position;
        newEnemy.transform.rotation = transform.rotation;
        newEnemy.GetComponent<PathController>()._points = _paths;
        newEnemy.gameObject.SetActive(true);
        
    }
}
