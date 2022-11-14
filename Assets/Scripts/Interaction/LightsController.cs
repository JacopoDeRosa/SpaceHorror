using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{
    [SerializeField] private List<GameLight> _lights;
    [SerializeField] private float _toggleOffset = 0.25f;

    public void ToggleLight()
    {
        StartCoroutine(ToggleLights());
    }


    private IEnumerator ToggleLights()
    {
        foreach(GameLight light in _lights)
        {
            light.Toggle();
            yield return new WaitForSeconds(_toggleOffset);
        }


        yield return null;
    }
}
