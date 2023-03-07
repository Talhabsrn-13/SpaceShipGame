using Space.Abstract.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviourObject<GameManager>
{
    private void Awake()
    {
        SingletonThisObject(this);
    }
    public void StopGame()
    {
        Time.timeScale = 0f;
    }
}
