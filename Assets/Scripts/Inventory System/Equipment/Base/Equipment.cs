using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceHorror.InventorySystem
{
    public class Equipment : MonoBehaviour
    {
        [SerializeField] private EquipmentSlot _slotA, _slotB, _slotC, _slotD;
        [SerializeField] private Animator _characterAnimator;
        [SerializeField] private Inventory _parentInventory;

        private PlayerInput _input;

        public Inventory ParentInventory { get => _parentInventory; }

        private void Awake()
        {
            _slotA = new EquipmentSlot(this);
            _slotB = new EquipmentSlot(this);
            _slotC = new EquipmentSlot(this);
            _slotD = new EquipmentSlot(this);
        }

        private void Start()
        {
            _input = FindObjectOfType<PlayerInput>();
            if(_input)
            {
                _input.actions["Equip A"].started += OnEquipA;
                _input.actions["Equip B"].started += OnEquipB;
                _input.actions["Equip C"].started += OnEquipC;
                _input.actions["Equip D"].started += OnEquipD;
            }
        }

        private void OnDisable()
        {
            if (_input)
            {
                _input.actions["Equip A"].started -= OnEquipA;
                _input.actions["Equip B"].started -= OnEquipB;
                _input.actions["Equip C"].started -= OnEquipC;
                _input.actions["Equip D"].started -= OnEquipD;
            }
        }

        public bool TrySetSlotItem(ItemSlot itemSlot, EquipmentSlot equipmentSlot)
        {
            if (equipmentSlot.Parent != this) return false;

            EquippableItemData data = itemSlot.ItemData as EquippableItemData;

            if (data == null) return false;

            EquippableItem item = Instantiate(data.Item, transform);

            equipmentSlot.SetItem(item);


            return true;
        }

        private void OnEquipA(InputAction.CallbackContext context)
        {

        }

        private void OnEquipB(InputAction.CallbackContext context)
        {

        }

        private void OnEquipC(InputAction.CallbackContext context)
        {

        }

        private void OnEquipD(InputAction.CallbackContext context)
        {

        }
    }
}
