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
    public class ItemSlotUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private Vector2 _optionsMenuOffset;

        private Inspector _inspector;
        private ItemSlot _targetSlot;
        private SlotOptionsMenu _optionsMenu;

        public void SetOptionsMenu(SlotOptionsMenu menu)
        {
            _optionsMenu = menu;
        }
        public void SetInspector(Inspector inspector)
        {
            _inspector = inspector;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (_optionsMenu == null) return;

                _optionsMenu.SetSlot(_targetSlot);
                _optionsMenu.AddSlotAction(new ButtonAction("Inspect", InspectSlot)); 
                _optionsMenu.ToggleVisible();
                _optionsMenu.transform.position = eventData.position + _optionsMenuOffset;

           //  print("Select " + _targetSlot.ItemData.name);
            }
            else if (eventData.button == PointerEventData.InputButton.Left)
            {
           //     print("Select " + _targetSlot.ItemData.name);
            }
        }

        private void InspectSlot()
        {
            _inspector.transform.position = _optionsMenu.transform.position;
            _inspector.ReadSlot(_targetSlot);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
         //   print("End Drag of" + _targetSlot.ItemData.name);
            _image.raycastTarget = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
           // print("Begin Drag of" + _targetSlot.ItemData.name);
            _image.raycastTarget = false;
        }
    }
}