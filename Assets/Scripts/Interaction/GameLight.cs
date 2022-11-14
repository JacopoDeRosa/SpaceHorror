using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLight : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private Light _light;

    public UnityEvent onToggle;


    private bool _isOn = false;

    private void Start()
    {
        _light.enabled = false;
    }

    public void Toggle()
    {
        if (_isOn)
        {
            _light.enabled = false;
            _isOn = false;
        }
        else
        {
            _light.enabled = true;
            _isOn = true;
        }

        _audioSource.PlayOneShot(_clip);
        onToggle.Invoke();
    }
}
