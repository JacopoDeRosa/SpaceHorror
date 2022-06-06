using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _nameText, _typeText, _weightText, _amountText;
    [SerializeField] private SlotOptionsMenu _optionsMenu;

    private bool _selected;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            _optionsMenu.ToggleVisible();
            _optionsMenu.transform.position = eventData.position;
        }
        else if(eventData.button == PointerEventData.InputButton.Left)
        {
            print("Select the item");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("Exit");
       // _optionsMenu.SetVisible(false);
    }
}