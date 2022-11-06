using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonitorDropdown : GameOptionSetter
{
    [SerializeField] private TMP_Dropdown _dropdown;

    private List<string> _displays;

    private int _targetChange;

    private void Start()
    {
        _dropdown.onValueChanged.AddListener(OnDropdownChange);
        _dropdown.ClearOptions();
        _displays = new List<string>(GetDisplays());
        _dropdown.AddOptions(_displays);

        Redraw();
    }
    private IEnumerable<string> GetDisplays()
    {
        Display[] monitors =  Display.displays;

        for (int i = 0; i < Display.displays.Length; i++)
        {
           
            int index = i + 1;
            if(i == 0)
            {
                yield return index + " (Main)" + " Requires Restart";
            }
            else
            {
                yield return index + " Requires Restart";
            }
   
        }
    }

    private void OnDropdownChange(int change)
    {
        _targetChange = change;
        SetOptionDirty();
    }

    public override void Redraw()
    {
        int activeDisplay = PlayerPrefs.GetInt("UnitySelectMonitor", 1);
        _dropdown.SetValueWithoutNotify(activeDisplay);
    }

    protected override void OnApply()
    {
        PlayerPrefs.SetInt("UnitySelectMonitor", _targetChange);
    }
}
