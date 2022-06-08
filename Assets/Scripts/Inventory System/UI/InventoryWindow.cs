using SpaceHorror.InventorySystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem.UI
{
    public class InventoryWindow : MonoBehaviour
    {
        public const int cellSizeX = 50;
        public const int cellSizeY = 50;

        [SerializeField] private ItemSlotUI[] _allSlots;
        [SerializeField] private InventoryCellUI[] _allCells;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private SlotOptionsMenu _optionsMenu;
        [SerializeField] private Inspector _inspector;

        private Queue<ItemSlotUI> _freeSlots;
        private Queue<InventoryCellUI> _freeCells;

        private void Start()
        {
            ResetWindow();
            ReadInventory(_inventory);
        }

        private void ResetWindow()
        {
            foreach (ItemSlotUI slot in _allSlots)
            {
                slot.SetOptionsMenu(_optionsMenu);
                slot.SetInspector(_inspector);
                slot.gameObject.SetActive(false);
            }
            foreach (InventoryCellUI cell in _allCells)
            {
                cell.gameObject.SetActive(false);
            }
            _freeSlots = new Queue<ItemSlotUI>(_allSlots);
            _freeCells = new Queue<InventoryCellUI>(_allCells);           
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


