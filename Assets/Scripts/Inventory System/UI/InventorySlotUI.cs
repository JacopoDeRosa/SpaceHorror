using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace SpaceHorror.InventorySystem.UI
{
    public class InventorySlotUI : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private TMP_Text _nameText, _typeText, _weightText, _amountText;
        [SerializeField] private SlotOptionsMenu _optionsMenu;
        [SerializeField] private Vector2 _optionsMenuOffset;

        private bool _selected;

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
    }
}