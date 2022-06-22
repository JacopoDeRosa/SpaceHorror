using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace SpaceHorror.InventorySystem
{
    public class ConsumableSlotUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Image _image;
        private Equipment _targetEquipment;

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

        private void RefreshAmountText()
        {

        }
    }
}
