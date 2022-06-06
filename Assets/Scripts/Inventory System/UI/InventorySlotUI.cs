using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Reflection;
using UnityEngine.EventSystems;

namespace SpaceHorror.InventorySystem.UI
{
    public class InventorySlotUI : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private TMP_Text _nameText, _typeText, _weightText, _amountText;
        [SerializeField] private SlotOptionsMenu _optionsMenu;
        [SerializeField] private Vector2 _optionsMenuOffset;

        private InventorySlot _targetSlot;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                _optionsMenu.ToggleVisible();
                _optionsMenu.transform.position = eventData.position + _optionsMenuOffset;
            }
            else if (eventData.button == PointerEventData.InputButton.Left)
            {
                print("Select the item");
            }
        }

        public void Init(InventorySlot slot)
        {
            _optionsMenu.InitButtons(slot.GetButtonActions());
            _nameText.text = slot.ItemData.name;
            _typeText.text = slot.ItemData.GetItemType().ToString();
            _weightText.text = slot.ItemData.Weight.ToString();
            _amountText.text = "x" + slot.ItemCount.ToString();
        }
    }
}