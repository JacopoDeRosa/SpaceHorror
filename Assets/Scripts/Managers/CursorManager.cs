using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private CursorLockMode _normalState;
    [SerializeField] private CursorLockMode _pausedState;


    private void Awake()
    {
        GameStatus.onMenuFocus += OnFocus;
        GameStatus.onMenuUnFocus += OnFocusEnd;
    }

    private void OnDisable()
    {
        GameStatus.onMenuFocus -= OnFocus;
        GameStatus.onMenuUnFocus -= OnFocusEnd;
    }

    private void Start()
    {
        Cursor.lockState = _normalState;
    }

    private void OnFocus()
    {
        Cursor.lockState = _pausedState;
    }

    private void OnFocusEnd()
    {
        Cursor.lockState = _normalState;
    }

}
