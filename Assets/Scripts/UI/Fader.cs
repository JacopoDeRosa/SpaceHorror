using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _fadeTime;
    [SerializeField] private Color _fadedColor, _clearColor;
    [SerializeField] private bool _fadeInAtStart;

    private bool _busy;

    private void Start()
    {
        if(_fadeInAtStart)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    public IEnumerator FadeInRoutine()
    {
        _image.raycastTarget = false;
        yield return SmoothFadeBetween(_fadedColor, _clearColor);
    }

    public IEnumerator FadeOutRoutine()
    {
        _image.raycastTarget = true;
        yield return SmoothFadeBetween(_clearColor, _fadedColor);
    }

    private IEnumerator SmoothFadeBetween(Color a, Color b)
    {
        if (_busy) yield break;

        _busy = true;

        float t = 0;
        while (t < 1)
        {
            _image.color = Color.Lerp(a, b, t);
            t += (1 / _fadeTime) * Time.fixedDeltaTime;
            
            yield return new WaitForFixedUpdate();
        }

        _busy = false;
    }
}
