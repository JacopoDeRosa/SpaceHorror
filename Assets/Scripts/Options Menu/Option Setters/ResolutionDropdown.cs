using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionDropdown : GameOptionSetter
{
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private List<CustomResolution> _supportedResolutions;

    private List<string> _dropdownOptions;

    private int _intendedRes;

    private void Start()
    {      
        _dropdown.onValueChanged.AddListener(OnDropdownChange);

        _dropdownOptions = new List<string>(GetResToString(_supportedResolutions));

        _dropdown.ClearOptions();

        _dropdown.AddOptions(_dropdownOptions);

        string currentResolution = new CustomResolution(Screen.width, Screen.height).ToString();

        _intendedRes = _dropdownOptions.FindIndex(x => x.Equals(currentResolution));

        Redraw();
    }

    private IEnumerable<string> GetResToString(IEnumerable<CustomResolution> resolutions)
    {
        foreach (var resolution in resolutions)
        {
            yield return resolution.ToString();
        }
    }

    private IEnumerable<string> GetAutoResToString()
    {
        foreach (var resolution in Screen.resolutions)
        {
            yield return new CustomResolution(resolution.width, resolution.height).ToString();
        }
    }

    public override void Redraw()
    {

        _dropdown.SetValueWithoutNotify(_intendedRes);
    }

    private void OnDropdownChange(int change)
    {
        SetOptionDirty();
        _intendedRes = change;
    }

    protected override void OnApply()
    {
        CustomResolution res = _supportedResolutions[_intendedRes];
        Screen.SetResolution(res.Width, res.Height, Screen.fullScreen);
    }
}

[System.Serializable]
public class CustomResolution
{
    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;

    public int Width { get => _width; }
    public int Height { get => _height; }

    public CustomResolution(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public override string ToString()
    {
        return _width.ToString() + " x " + _height.ToString();
    }
}

