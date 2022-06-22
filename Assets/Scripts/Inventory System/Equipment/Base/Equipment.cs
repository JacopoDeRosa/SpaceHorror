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
        [SerializeField] private Transform _itemContainer;

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

        public bool TrySetEquippableItem(ItemSlot itemSlot, EquipmentSlotType type)
        {
            if(type == EquipmentSlotType.Primary)
            {
                return TrySetEquippableItem(itemSlot, _equippableA);
            }
            else if(type == EquipmentSlotType.Secondary)
            {
                return TrySetEquippableItem(itemSlot, _equippableB);
            }

            return false;
        }

        public bool TrySetEquippableItem(ItemSlot itemSlot, EquippableSlot slot)
        {
            if (slot.Item != null) return false;

            EquippableItemData equippableItemData = itemSlot.ItemData as EquippableItemData;

            if (equippableItemData == null) return false;

            EquippableItem item = Instantiate(equippableItemData.Item, _itemContainer);

            item.LoadParameters(itemSlot.ItemParameters);

            slot.SetItem(item);

            return true;
        }

        public bool TrySetConsumableItem(ItemSlot itemSlot, EquipmentSlotType type)
        {
            if (type == EquipmentSlotType.Primary)
            {
                return TrySetConsumableItem(itemSlot, _consumableA);
            }
            else if (type == EquipmentSlotType.Secondary)
            {
                return TrySetConsumableItem(itemSlot, _consumableB);
            }

            return false;
        }

        public bool TrySetConsumableItem(ItemSlot itemSlot, ConsumableSlot slot)
        {
            if (slot.ActiveSlot != null) return false;

            if(itemSlot.ItemData is ConsumableItemData)
            {
                slot.SetSlot(itemSlot);        
            }
            return true;
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
