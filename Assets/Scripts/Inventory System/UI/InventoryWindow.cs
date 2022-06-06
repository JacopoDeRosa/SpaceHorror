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

        private Queue<InventorySlotUI> _freeSlots;


        private void Start()
        {
            ResetWindow();
            ReadInventory(_inventory);
        }

        private void ResetWindow()
        {
            foreach (InventorySlotUI slot in _allSlots)
            {
                slot.gameObject.SetActive(false);
            }

            _freeSlots = new Queue<InventorySlotUI>(_allSlots);
        }

        public void SetInventory(Inventory inventory)
        {

        }

        public void ClearInventory(Inventory inventory)
        {

        }

        private void ReadInventory(Inventory inventory)
        {
            foreach (InventorySlot slot in inventory.InventorySlots)
            {
                if (_freeSlots.Count == 0) break;

                var uiSlot = _freeSlots.Dequeue();
                uiSlot.gameObject.SetActive(true);
                uiSlot.Init(slot);
            }
        }
    }
}


