using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameStatus
{
    public static bool Paused { get; private set; }
    public static bool MenuFocused { get; private set; }

    public static event Action onPauseStart;
    public static event Action onPauseEnd;

    public static event Action onMenuFocus;
    public static event Action onMenuUnFocus;

    public static void SetPaused(bool paused)
    {
        if (Paused == paused) return;

        if(paused)
        {        
            onPauseStart?.Invoke();
            onMenuFocus?.Invoke();
        }
        else
        {
            onPauseEnd?.Invoke();
            onMenuUnFocus?.Invoke();
        }

        Paused = paused;
    }

    public static void SetMenuFocus(bool focus)
    {
        if (MenuFocused == focus) return;

        if(focus)
        {
            onMenuFocus?.Invoke();
        }
        else
        {
            onMenuUnFocus?.Invoke();
        }

        MenuFocused = focus;
    }
}
