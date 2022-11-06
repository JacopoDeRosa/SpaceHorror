using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenToggle : GameOptionSetter
{
    [SerializeField] private Toggle _toggle;

    private void Start()
    {
        _toggle.onValueChanged.AddListener(OnToggle);
        Redraw();
    }

    public override void Redraw()
    {
       
    }

    private void OnToggle(bool status)
    {
        SetOptionDirty();
    }

    protected override void OnApply()
    {
        Screen.fullScreen = _toggle.isOn;
    }
}
