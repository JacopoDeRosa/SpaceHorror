using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private int _currentLevel;
    [SerializeField] private int _maxLevels, _minLevels;

    public void OnMapOpen()
    {
        _camera.gameObject.SetActive(true);
    }
    public void OnMapClose()
    {
        _camera.gameObject.SetActive(false);
    }
}
