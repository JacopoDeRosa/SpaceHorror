using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class SlotOptionsButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TMP_Text _text;


    public Action onPress; 

    public void Init(string name)
    {
        _text.text = name;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {

        }
    }
}
