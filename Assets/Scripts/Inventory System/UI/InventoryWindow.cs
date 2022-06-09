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
        [SerializeField] private int _cellsBorder;
        [SerializeField] private Vector2Int _padding;
        [SerializeField] private RectTransform _cellsContainer, _slotsContainer;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private SlotOptionsMenu _optionsMenu;
        [SerializeField] private Inspector _inspector;

        private Queue<ItemSlotUI> _freeSlots;
        private Queue<CellUI> _freeCells;

        private CellUI[,] _cellsGrid;

        private void Start()
        {
            if (_inventory == null)
            {
                ResetWindow();
            }
            else
            {
                ReadInventory(_inventory);
            }
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
            ResetWindow();
            _cellsGrid = new CellUI[inventory.Size.x, inventory.Size.y];

            for (int y = 0; y < inventory.Size.y; y++)
            {
                for (int x = 0; x < inventory.Size.x; x++)
                {
                    CellUI cell = _freeCells.Dequeue();

                    cell.gameObject.SetActive(true);
                    cell.SetTargetCell(inventory.GetCell(x, y));

                    int posX = (int)-(_cellsContainer.rect.width / 2);
                    int posY = (int)(_cellsContainer.rect.height / 2);

                    posX += _padding.x;
                    posY -= _padding.y;

                    posX += cellSizeX / 2;
                    posY -= cellSizeY / 2;

                    posX += (cellSizeX + _cellsBorder) * x;
                    posY -= (cellSizeY + _cellsBorder) * y;

                    cell.transform.localPosition = new Vector3Int(posX, posY);
                    _cellsGrid[x, y] = cell;
                }
            }
            foreach (ItemSlot slot in inventory.AllItems)
            {
                var uiSlot = _freeSlots.Dequeue();
                uiSlot.gameObject.SetActive(true);
                uiSlot.SetItemSlot(slot);
                uiSlot.transform.position = _cellsGrid[slot.Position.x, slot.Position.y].transform.position;
            }
        }
    }
}


