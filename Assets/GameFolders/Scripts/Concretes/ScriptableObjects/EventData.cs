using Space.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "Data/Event Data")]
public class EventData : ScriptableObject
{
    PlayerController player;

    public PlayerController Player
    {
        get => player;
        set => player = value;
    }

    public Action OnPlay;
    public Action OnWin;
    public Action OnLose;
    public Action OnIdle;
}
