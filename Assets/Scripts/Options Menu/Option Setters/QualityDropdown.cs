using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QualityDropdown : GameOptionSetter
{
    [SerializeField] private TMP_Dropdown _dropdown;

    private List<string> _qualityLevels;

    private int _intendedChange;

    private void Start()
    {
        _dropdown.onValueChanged.AddListener(OnDropdownChange);
        _dropdown.ClearOptions();
        _qualityLevels = new List<string>(GetQualityLevels());
        _dropdown.AddOptions(_qualityLevels);
        Redraw();
    }

    private IEnumerable<string> GetQualityLevels()
    {
       return QualitySettings.names;
    }

    public override void Redraw()
    {
        _dropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
    }

    private void OnDropdownChange(int change)
    {
        _intendedChange = change;
        SetOptionDirty();
    }

    protected override void OnApply()
    {
        QualitySettings.SetQualityLevel(_intendedChange);
    }
}
