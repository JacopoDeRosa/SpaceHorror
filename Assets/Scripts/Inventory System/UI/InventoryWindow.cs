using SpaceHorror.InventorySystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem.UI
{
    public class InventoryWindow : MonoBehaviour
    {
        public const int cellSizeX = 100;
        public const int cellSizeY = 100;

        [SerializeField] private ItemSlotUI[] _allSlots;
        [SerializeField] private CellUI[] _allCells;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private SlotOptionsMenu _optionsMenu;
        [SerializeField] private Inspector _inspector;

        private Queue<ItemSlotUI> _freeSlots;
        private Queue<CellUI> _freeCells;

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
            foreach (CellUI cell in _allCells)
            {
                cell.gameObject.SetActive(false);
            }
            _freeSlots = new Queue<ItemSlotUI>(_allSlots);
            _freeCells = new Queue<CellUI>(_allCells);           
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


