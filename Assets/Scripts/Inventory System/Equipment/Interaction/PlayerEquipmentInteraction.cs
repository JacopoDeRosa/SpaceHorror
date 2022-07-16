using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceHorror.InventorySystem.Interaction
{
    public class PlayerEquipmentInteraction : MonoBehaviour
    {
        [SerializeField] private Equipment _playerEquipment;

        private PlayerInput _input;

        private void Start()
        {
            _input = FindObjectOfType<PlayerInput>();

            if(_input)
            {
                _input.actions["Equip A"].started += OnEquipA;
                _input.actions["Equip B"].started += OnEquipB;
                _input.actions["Remove Equipped"].started += OnRemoveEquipped;
                _input.actions["Fire"].started += OnFire;
                _input.actions["Utility"].started += OnUtility;
            }
        }

        private void OnDisable()
        {
            if (_input)
            {
                _input.actions["Equip A"].started -= OnEquipA;
                _input.actions["Equip B"].started -= OnEquipB;
                _input.actions["Remove Equipped"].started -= OnRemoveEquipped;
                _input.actions["Fire"].started -= OnFire;
                _input.actions["Utility"].started -= OnUtility;
            }
        }

        private void OnEquipA(InputAction.CallbackContext context)
        {
            _playerEquipment.ActivateSlot(EquipmentSlotType.Primary);
        }

        private void OnEquipB(InputAction.CallbackContext context)
        {
            _playerEquipment.ActivateSlot(EquipmentSlotType.Secondary);
        }

        private void OnRemoveEquipped(InputAction.CallbackContext context)
        {
            _playerEquipment.ClearActiveSlot();
        }

        private void OnFire(InputAction.CallbackContext context)
        {
            if(_playerEquipment.GetActiveSlotItem())
            {
                _playerEquipment.GetActiveSlotItem().PrimaryUse();
            }
        }

        private void OnUtility(InputAction.CallbackContext context)
        {
            if (_playerEquipment.GetActiveSlotItem())
            {
                _playerEquipment.GetActiveSlotItem().UtilityUse();
            }
        }
    }
}
