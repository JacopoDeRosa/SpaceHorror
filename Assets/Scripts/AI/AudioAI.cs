using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAI : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _screams;
    [SerializeField] private List<AudioClip> _growls;
    [SerializeField] private AudioSource _audioSource;
    [Tooltip("Time in between each sound")]
    [SerializeField] private int _coolDownTime;

    private bool _coolingDown = false;

    public void Scream()
    {
        if (_coolingDown) return;
        AudioClip scream = _screams[Random.Range(0, _screams.Count)];
        _audioSource.Stop();
        _audioSource.PlayOneShot(scream);
        StartCooldown();
    }

    public void Growl()
    {
        if (_coolingDown) return;
        AudioClip growl = _growls[Random.Range(0, _growls.Count)];
        _audioSource.Stop();
        _audioSource.PlayOneShot(growl);
        StartCooldown();
    }

    private void StartCooldown()
    {
        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown()
    {
        _coolingDown = true;

        yield return new WaitForSeconds(_coolDownTime);

        _coolingDown = false;
    }
}
