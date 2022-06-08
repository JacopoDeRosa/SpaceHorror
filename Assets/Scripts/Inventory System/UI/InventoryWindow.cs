using SpaceHorror.InventorySystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem.UI
{
    public class InventoryWindow : MonoBehaviour
    {
        [SerializeField] private InventorySlotUI[] _allSlots;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private SlotOptionsMenu _optionsMenu;
        [SerializeField] private Inspector _inspector;


        private void Start()
        {
            ResetWindow();
            ReadInventory(_inventory);
        }

        private void ResetWindow()
        {
            foreach (InventorySlotUI slot in _allSlots)
            {
                slot.SetOptionsMenu(_optionsMenu);
                slot.SetInspector(_inspector);
            }
        }

        public void SetInventory(Inventory inventory)
        {

        }

        public void ClearInventory(Inventory inventory)
        {

        }

        private void ReadInventory(Inventory inventory)
        {
         
        }
    }
}


