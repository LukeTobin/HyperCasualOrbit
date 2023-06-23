using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public UnityEvent onPlayerDied = new UnityEvent();
    public UnityEvent onGameStarted = new UnityEvent();
    public UnityEvent onPlayerFinishedJump = new UnityEvent();
}