using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem.UI
{
    public class EquipmentWindow : MonoBehaviour
    {
        [SerializeField] private Equipment _target;
        [SerializeField] private EquippableSlotUI _equippableA, _equippableB;
        [SerializeField] private ConsumableSlotUI _consumableA, _consumableB;

        private void Awake()
        {
            _equippableA.SetTargetEquipment(_target);
            _equippableA.SetSlotType(EquipmentSlotType.Primary);

            _equippableB.SetTargetEquipment(_target);
            _equippableB.SetSlotType(EquipmentSlotType.Secondary);

            _consumableA.SetTargetEquipment(_target);
            _consumableA.SetSlotType(EquipmentSlotType.Primary);

            _consumableB.SetTargetEquipment(_target);
            _consumableB.SetSlotType(EquipmentSlotType.Secondary);
        }

        private void SetTargetEquipment(Equipment target)
        {

        }

    }
}
