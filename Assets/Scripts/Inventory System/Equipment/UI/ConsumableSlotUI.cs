using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class ConsumableSlotUI : MonoBehaviour
    {
        private EquipmentSlotType _slotType;

        public EquipmentSlotType SlotType { get => _slotType; }

        public void SetSlotType(EquipmentSlotType slotType)
        {
            _slotType = slotType;
        }
    }
}
