using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace SpaceHorror.InventorySystem
{
    public class EquippableSlotUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Image _image;

        private Equipment _targetEquipment;

        public EquipmentSlotType SlotType { get; private set; }


        public void SetSlotType(EquipmentSlotType slotType)
        {
            SlotType = slotType;
        }

        public bool TrySetItem(ItemSlot slot)
        {
            if (_targetEquipment == null) return false;
            if (slot == null) return false;

            if(_targetEquipment.TrySetEquippableItem(slot, SlotType))
            {
                _nameText.text = slot.ItemData.name;
                _image.sprite = slot.ItemData.Icon;
                return true;
            }

            return false;
        }
    }
}
