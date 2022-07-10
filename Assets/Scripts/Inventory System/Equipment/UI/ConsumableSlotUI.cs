using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpaceHorror.InventorySystem.UI
{
    public class ConsumableSlotUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private ItemRemoverUI _itemRemover;

        private Equipment _targetEquipment;
        private bool _dragging;

        public EquipmentSlotType SlotType { get; private set; }

        public void SetSlotType(EquipmentSlotType slotType)
        {
            SlotType = slotType;
        }

        public void SetTargetEquipment(Equipment equipment)
        {
            _targetEquipment = equipment;
        }

        public bool TrySetItem(ItemSlot slot)
        {
            if (_targetEquipment == null) return false;
            if (slot == null) return false;

            if (_targetEquipment.TrySetConsumableItem(slot, SlotType))
            {
                _amountText.text = "x" + slot.ItemCount.ToString();
                _nameText.text = slot.ItemData.name;
                _image.sprite = slot.ItemData.Icon;
                return true;
            }

            return false;
        }

        private void RefreshAmountText(ItemSlot slot)
        {
            _amountText.text = "x" + slot.ItemCount.ToString();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            ItemSlot slot = _targetEquipment.ClearConsumableSlot(SlotType);
            if (slot == null)
            {
                return;
            }
            _dragging = true;
            _image.sprite = _defaultSprite;
            _nameText.text = "Empty Slot";
            _amountText.text = "x00";



            _itemRemover.gameObject.SetActive(true);
            _itemRemover.SetItemSlot(slot);
            _itemRemover.transform.position = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_dragging == false) return;
            _itemRemover.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_dragging == false) return;
            CellUI cell = _itemRemover.GetItemAtPivot<CellUI>();
            _dragging = false;

            if (cell == null)
            {
                Debug.Log("Cell Null");
                TrySetItem(_itemRemover.Slot);
                _itemRemover.gameObject.SetActive(false);
            }
            else
            {
                if (_itemRemover.Slot.DropInInventory(cell.TargetCell))
                {
                    _itemRemover.gameObject.SetActive(false);
                }
            }
           
        }
    }
}
