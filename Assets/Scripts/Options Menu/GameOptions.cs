using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameOptions : MonoBehaviour
{
    [SerializeField] private Button _applyButton;

    private void Start()
    {
        _applyButton.onClick.AddListener(Apply);
    }

    public void Redraw()
    {
        foreach (var item in FindObjectsOfType<GameOptionSetter>())
        {
            item.Redraw();
        }  
    }

    private void Apply()
    {
        var setters = FindObjectsOfType<GameOptionSetter>();
        foreach (var setter in setters)
        {
            setter.Apply();
        }
    }

    public void Revert()
    {

    }
 
}
