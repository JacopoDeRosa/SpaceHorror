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
        [SerializeField] private List<GameObject> _hoverStack;

        private Inspector _inspector;
        private ItemSlot _targetSlot;
        private SlotOptionsMenu _optionsMenu;
        private InventoryWindow _parentWindow;

        private Vector2 _dragOffset;
        private Vector2Int CenterOffset
        {
            get
            {

                Vector2Int position = Vector2Int.zero;

                if (_targetSlot == null) return position;

                if (_targetSlot.Size.x % 2 == 0)
                {
                    position.x += (InventoryWindow.cellSizeX / 2) + InventoryWindow.cellsBorder / 2;
                }

                if (_targetSlot.Size.y % 2 == 0)
                {
                    position.y += (InventoryWindow.cellSizeY / 2) + InventoryWindow.cellsBorder / 2;
                }

                return position;
            } 
        }

        private CellUI _starterCell;

        public ItemSlotUIHandler onSlotDeath;

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
            _targetSlot.onDestroy += OnTargetDeath;
        }

        public void SetParentWindow(InventoryWindow window)
        {
            _parentWindow = window;
        }

        private void OnTargetDeath()
        {
            onSlotDeath?.Invoke(this);
        }

        private void UpdateTexts(ItemSlot slot)
        {
            _nameText.text = slot.ItemData.name;
            _amountText.text = "x" + slot.ItemCount.ToString();
        }

        private void UpdateTexts()
        {
            UpdateTexts(_targetSlot);
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
                float sizeX = size.x * InventoryWindow.cellSizeX;
                float sizeY = size.y * InventoryWindow.cellSizeY;

                sizeX += InventoryWindow.cellsBorder * size.x;
                sizeY += InventoryWindow.cellsBorder * size.y;

                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeX);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeY);   
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
            transform.SetAsFirstSibling();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _backgroundImage.raycastTarget = false;
            _dragOffset = new Vector2(transform.position.x, transform.position.y) - eventData.position;
            _targetSlot.LiftFromInventory();
            _starterCell = GetCellAtPivot();
            transform.SetAsLastSibling();
        }

        public void SetPosition(Vector2 position)
        {
            position += CenterOffset;
            transform.position = position;
        }

        private List<RaycastResult> GetAllElementsAtPivot()
        {
            PointerEventData fakeData = new PointerEventData(EventSystem.current);

            fakeData.position = transform.position + new Vector3(-CenterOffset.x, -CenterOffset.y);

            List<RaycastResult> raycastResults = new List<RaycastResult>();

            EventSystem.current.RaycastAll(fakeData, raycastResults);

            return raycastResults;
        }

        private CellUI GetCellAtPivot()
        {
            CellUI possibleCell = null;
            foreach (RaycastResult result in GetAllElementsAtPivot())
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

        private ItemSlotUI GetSlotAtPivot()
        {
            ItemSlotUI possibleSlot = null;
            foreach (RaycastResult result in GetAllElementsAtPivot())
            {
               
            }
            return null;
        }

        private GameObject GetUiElementAtPivot()
        {
            return null;
        }

        private void Update()
        {
            List<GameObject> hover = new List<GameObject>();
            foreach (var item in GetAllElementsAtPivot())
            {
                hover.Add(item.gameObject);
            }
            _hoverStack = hover;
        }
    }
}