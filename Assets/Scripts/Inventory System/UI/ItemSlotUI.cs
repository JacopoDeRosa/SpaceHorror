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
        [SerializeField] private Image _backgroundImage, _spriteImage;
        [SerializeField] private TMP_Text _nameText, _amountText;
        [SerializeField] private Vector2 _optionsMenuOffset;

        private Inspector _inspector;
        private ItemSlot _targetSlot;
        private SlotOptionsMenu _optionsMenu;

        private Vector2 _dragOffset;

        public void SetOptionsMenu(SlotOptionsMenu menu)
        {
            _optionsMenu = menu;
        }

        public void SetInspector(Inspector inspector)
        {
            _inspector = inspector;
        }

        public void SetItemSlot(ItemSlot slot)
        {
            SetSize(slot.Size);
            UpdateTexts(slot);
            _spriteImage.sprite = slot.ItemData.Icon;
            _targetSlot = slot;
        }

        private void UpdateTexts(ItemSlot slot)
        {
            _nameText.text = slot.ItemData.name;
            _amountText.text = "x" + slot.ItemCount.ToString();
        }

        private void InspectSlot()
        {
            _inspector.transform.position = _optionsMenu.transform.position;
            _inspector.ReadSlot(_targetSlot);
        }

        private void SetSize(Vector2 size)
        {
            RectTransform rectTransform = transform as RectTransform;
            if(rectTransform)
            {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x * InventoryWindow.cellSizeY);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y * InventoryWindow.cellSizeY);
         
            }
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
            }
            else if (eventData.button == PointerEventData.InputButton.Left)
            {

            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position + _dragOffset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var possibleCell = eventData.hovered[eventData.hovered.Count - 1];
           if(possibleCell)
           {

           }
            _backgroundImage.raycastTarget = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _backgroundImage.raycastTarget = false;
            _dragOffset = new Vector2(transform.position.x, transform.position.y) - eventData.position;
        }

        public void SetPosition(Vector2 postion)
        {
            if (_targetSlot.Size.x % 2 == 0)
            {
                postion.x += InventoryWindow.cellSizeX / 2;
            }

            if (_targetSlot.Size.y % 2 == 0)
            {
                postion.y += InventoryWindow.cellSizeY / 2;
            }

            transform.position = postion;

        }
    }
}