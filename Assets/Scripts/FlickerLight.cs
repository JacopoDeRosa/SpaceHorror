using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    [SerializeField] private float _min, _max;
    [SerializeField] private Light _light;


    private void OnEnable()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while(true)
        {
            _light.intensity = Random.Range(_min, _max);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.25f));
        }
    }
}
