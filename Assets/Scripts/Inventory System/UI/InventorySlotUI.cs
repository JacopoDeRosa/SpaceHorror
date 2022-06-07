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
        [SerializeField] private Image _background;

        [SerializeField] private Vector2 _optionsMenuOffset;

        private InventorySlot _targetSlot;
        private SlotOptionsMenu _optionsMenu;

        public void SetOptionsMenu(SlotOptionsMenu menu)
        {
            _optionsMenu = menu;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (_optionsMenu == null) return;

                _optionsMenu.SetSlot(_targetSlot);
                _optionsMenu.ToggleVisible();
                _optionsMenu.transform.position = eventData.position + _optionsMenuOffset;
                print("Select " + _targetSlot.ItemData.name);
            }
            else if (eventData.button == PointerEventData.InputButton.Left)
            {
                print("Select " + _targetSlot.ItemData.name);
            }
        }

        public void Init(InventorySlot slot)
        {
            _nameText.text = slot.ItemData.name;
            _typeText.text = slot.ItemData.GetItemType().ToString();
            _weightText.text = slot.ItemData.Weight.ToString();
            _amountText.text = "x" + slot.ItemCount.ToString();
            _targetSlot = slot;
        }
    }
}