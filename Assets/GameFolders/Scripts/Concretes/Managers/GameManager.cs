using Space.Abstract.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviourObject<GameManager>
{
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
    }


    public void StopGame()
    {
        Time.timeScale = 0f;
    }
}
