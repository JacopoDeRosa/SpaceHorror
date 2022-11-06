using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameOptionSetter : MonoBehaviour
{
    private bool _dirty;

    protected void SetOptionDirty()
    {
        _dirty = true;
    }

    public virtual void Redraw()
    {

    }

    public void Apply()
    {
        if (_dirty == false) return;
        _dirty = false;
        OnApply();
        Redraw();
    }

    protected virtual void OnApply()
    {

    }

    public virtual void Revert()
    {

    }
}

