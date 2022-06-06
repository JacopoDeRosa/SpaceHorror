using SpaceHorror.InventorySystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem.UI
{
    public class InventoryWindow : MonoBehaviour
    {
        [SerializeField] private InventorySlotUI[] _allSlots;

        private Queue<InventorySlotUI> _freeSlots;

        private void Start()
        {
            ResetWindow();
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
    }
}


