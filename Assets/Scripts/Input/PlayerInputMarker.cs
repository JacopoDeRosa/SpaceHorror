using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputMarker : MonoBehaviour
{
    private void Awake()
    {
        if(FindObjectsOfType<PlayerInputMarker>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
