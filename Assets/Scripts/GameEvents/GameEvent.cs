using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "New Event", menuName = "New Event")]
public class GameEvent : ScriptableObject
{
  public event Action onInvoke;

  public void Invoke()
  {
        onInvoke?.Invoke();
  }
}