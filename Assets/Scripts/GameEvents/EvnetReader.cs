using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EvnetReader : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;

    public UnityEvent onEvent;


    private void Awake()
    {
        gameEvent.onInvoke += OnEventCalled;
    }

    private void OnEventCalled()
    {
        onEvent.Invoke();
    }

}
