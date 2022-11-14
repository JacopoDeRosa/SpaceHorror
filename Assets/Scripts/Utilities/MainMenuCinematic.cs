using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MainMenuCinematic : MonoBehaviour
{
    private static bool hasStarted = false;

    [SerializeField] private PlayableDirector _firstDirector, _mainDirector;


    private void Awake()
    {
        if(hasStarted)
        {
            _mainDirector.Play();
        }
        else
        {
            _firstDirector.Play();
            hasStarted = true;
        }
    }
}
