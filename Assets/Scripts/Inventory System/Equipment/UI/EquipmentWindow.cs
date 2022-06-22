using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class EquipmentWindow : MonoBehaviour
    {
        [SerializeField] private Equipment _target;
        [SerializeField] private EquippableSlotUI _equippableA, _equippableB;
        [SerializeField] private ConsumableSlotUI _consumableA, _consumableB;

        private void Awake()
        {
            
        }

        private void SetTargetEquipment(Equipment target)
        {

        }

    }
}
