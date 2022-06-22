using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceHorror.InventorySystem
{
    public class Equipment : MonoBehaviour
    {
        [SerializeField] private EquippableSlot _equippableA, _equippableB;
        [SerializeField] private ConsumableSlot _consumableA, _consumableB;
        [SerializeField] private Animator _characterAnimator;
        [SerializeField] private Inventory _parentInventory;

        private EquipmentSlotType _activeEquipmentSlot;

        private PlayerInput _input;

        public Inventory ParentInventory { get => _parentInventory; }

        
        private void Start()
        {
            _input = FindObjectOfType<PlayerInput>();
            if(_input)
            {
                _input.actions["Equip A"].started += OnEquipA;
                _input.actions["Equip B"].started += OnEquipB;
            }
        }

        private void OnDisable()
        {
            if (_input)
            {
                _input.actions["Equip A"].started -= OnEquipA;
                _input.actions["Equip B"].started -= OnEquipB;
            }
        }

        public bool TrySetEquippableSlotItem(ItemSlot itemSlot, EquipmentSlotType type)
        {
            if(type == EquipmentSlotType.Primary)
            {
                return TrySetEquippableSlotItem(itemSlot, _equippableA);
            }
            else if(type == EquipmentSlotType.Secondary)
            {
                return TrySetEquippableSlotItem(itemSlot, _equippableB);
            }

            return false;
        }

        public bool TrySetEquippableSlotItem(ItemSlot itemSlot, EquippableSlot slot)
        {
            if (slot.Item != null) return false;
            return false;
        }

        private void ActivateSlot(EquipmentSlotType slotType)
        {

        }

        private void OnEquipA(InputAction.CallbackContext context)
        {
            ActivateSlot(EquipmentSlotType.Primary);
        }

        private void OnEquipB(InputAction.CallbackContext context)
        {
            ActivateSlot(EquipmentSlotType.Secondary);
        }
    }
}
