using Space.Abstract.Controller;
using Space.Abstract.Entity;
using Space.Abstract.Movements;
using Space.Movements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Space.Controller
{
    public class CollectableController : MonoBehaviour, IEntityController, ICollectable
    {
        [SerializeField] float _speed;
        [SerializeField] CollectableType _collectableType;
        VerticalMover _mover;
        float _currentTime=0;
        float _maxTime = 10f;
        public Transform Transform => this.transform;

        public CollectableType CollectableType => _collectableType;

        EventData _eventData;
        private void Awake()
        {
            _mover = new VerticalMover(this);
            _eventData = Resources.Load("EventData") as EventData;
        }
        private void FixedUpdate()
        {
            if (!GameManager.Instance.Playability()) return;
            _currentTime += Time.deltaTime;
            if (_currentTime >_maxTime)
            {
                _currentTime = 0;
                KillYourSelf();
            }
            _mover.FixedTick(false, _speed);
        }
       
        public void KillYourSelf()
        {
            Managers.CollectableManager.Instance.SetPool(this);
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            _eventData.OnWin += KillYourSelf;
            _eventData.OnLose += KillYourSelf;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            _eventData.OnWin -= KillYourSelf;
            _eventData.OnLose -= KillYourSelf;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            KillYourSelf();
        }
    }
}
