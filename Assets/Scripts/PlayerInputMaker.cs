using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputMaker : MonoBehaviour
{
    private void Awake()
    {
        if(FindObjectsOfType<PlayerInputMaker>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
