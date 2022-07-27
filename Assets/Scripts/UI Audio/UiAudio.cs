using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clickClips;
    [SerializeField] private AudioSource _audioSource;
    private AudioClip RandomClickClip
    {
        get => _clickClips[Random.Range(0, _clickClips.Length)];
    }

    private void Awake()
    {
        foreach (Button button in FindObjectsOfType<Button>(true))
        {
            button.onClick.AddListener(PlayRandomClickClip);
        }
    }
  
    private void PlayRandomClickClip()
    {
        _audioSource.PlayOneShot(RandomClickClip);
    }
}
