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
        private Vector2Int CenterOffset
        {
            get
            {

                Vector2Int position = Vector2Int.zero;

                if (_targetSlot == null) return position;

                if (_targetSlot.Size.x % 2 == 0)
                {
                    position.x += InventoryWindow.cellSizeX / 2;
                }

                if (_targetSlot.Size.y % 2 == 0)
                {
                    position.y += InventoryWindow.cellSizeY / 2;
                }

                return position;
            } 
        }

        private CellUI _starterCell;

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
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position + _dragOffset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            CellUI possibleCell = GetCellAtPivot();

            if(possibleCell)
            {
                if(_targetSlot.DropInInventory(possibleCell.TargetCell))
                {
                    SetPosition(possibleCell.transform.position);
                }
                else
                {
                    _targetSlot.DropBack();
                    SetPosition(_starterCell.transform.position);
                }
            }
            else
            {
                _targetSlot.DropBack();
                SetPosition(_starterCell.transform.position);
            }
            _starterCell = null;
            _backgroundImage.raycastTarget = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _backgroundImage.raycastTarget = false;
            _dragOffset = new Vector2(transform.position.x, transform.position.y) - eventData.position;
            _targetSlot.LiftFromInventory();
            _starterCell = GetCellAtPivot();
        }

        public void SetPosition(Vector2 position)
        {
            position += CenterOffset;
            transform.position = position;
        }

        private CellUI GetCellAtPivot()
        {
            PointerEventData fakeData = new PointerEventData(EventSystem.current);
            fakeData.position = transform.position + new Vector3(-CenterOffset.x, -CenterOffset.y);
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(fakeData, raycastResults);
            CellUI possibleCell = null;
            foreach (RaycastResult result in raycastResults)
            {
                var cell = result.gameObject.GetComponent<CellUI>();
                if (cell != null)
                {
                    possibleCell = cell;
                    break;
                }
            }

            return possibleCell;
        }
    }
}