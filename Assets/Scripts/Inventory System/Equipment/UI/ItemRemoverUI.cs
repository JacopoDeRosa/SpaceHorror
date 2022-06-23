using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace SpaceHorror.InventorySystem.UI
{
    public class ItemRemoverUI : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemName;

        private ItemSlot _targetSlot;

        public ItemSlot Slot { get => _targetSlot; }

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

        public void Init(ItemSlot slot)
        {
            if(slot == null)
            {
                return;
            }

            _targetSlot = slot;
            SetSize(_targetSlot.Size);
            UpdateTexts(_targetSlot);
        }

        private List<RaycastResult> GetAllElementsAtPivot()
        {
            PointerEventData fakeData = new PointerEventData(EventSystem.current);

            fakeData.position = transform.position + new Vector3(-CenterOffset.x, -CenterOffset.y);

            List<RaycastResult> raycastResults = new List<RaycastResult>();

            EventSystem.current.RaycastAll(fakeData, raycastResults);

            return raycastResults;
        }

        public T GetItemAtPivot<T>() where T : class
        {
            T possibleItem = null;
            foreach (RaycastResult result in GetAllElementsAtPivot())
            {
                var slot = result.gameObject.GetComponent<T>();
                if (slot != null)
                {
                    possibleItem = slot;
                }
            }
            return possibleItem;
        }

        public void SetItemSlot(ItemSlot slot)
        {
            if (slot == null)
            {
                _targetSlot = null;
                return;
            }
            SetSize(slot.Size);
            UpdateTexts(slot);
            _itemIcon.sprite = slot.ItemData.Icon;
            _targetSlot = slot;
        }

        private void SetSize(Vector2 size)
        {
            RectTransform rectTransform = transform as RectTransform;
            if (rectTransform)
            {
                float sizeX = size.x * InventoryWindow.cellSizeX;
                float sizeY = size.y * InventoryWindow.cellSizeY;

                sizeX += InventoryWindow.cellsBorder * size.x;
                sizeY += InventoryWindow.cellsBorder * size.y;

                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeX);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeY);
            }
        }

        private void UpdateTexts(ItemSlot slot)
        {
            _itemName.text = slot.ItemData.name;
        }

    }
}
