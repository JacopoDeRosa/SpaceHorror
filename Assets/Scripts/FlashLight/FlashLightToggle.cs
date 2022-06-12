using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightToggle : MonoBehaviour
{

    [SerializeField] private KeyCode _toggleKey;
    private Light _light;

    void Awake()
    {
        _light = GetComponent<Light>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(_toggleKey))
        {
            _light.enabled = !_light.enabled;
        }
    }
}
