using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SpaceHorror.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private Vector2Int _size;
        [SerializeField] private List<ItemSlot> _initialItems;

        private List<ItemSlot> _allItems;
        private Cell[,] _cells;

        public string Name { get => _name; }
        public Cell[,] InventoryCells { get => _cells; }
        public Vector2Int Size { get => _size; }
        public IEnumerable<ItemSlot> AllItems { get => new List<ItemSlot>(_allItems); }


        public event ItemSlotHandler onSlotAdded;

        public float GetTotalWeight()
        {
            float weight = 0;
            foreach (ItemSlot item in _allItems)
            {
                weight += item.ItemCount * item.ItemData.Weight;
            }
            return weight;
        }

        private void Awake()
        {
            _allItems = new List<ItemSlot>();
            _cells = new Cell[_size.x, _size.y];

            for (int y = 0; y < _size.y; y++)
            {
                for (int x = 0; x < _size.x; x++)
                {
                    _cells[x,y] = new Cell(x, y);
                }
            }

            foreach (ItemSlot item in _initialItems)
            {
                if (TryPlaceItem(item) == false) break;
            }
        }

        public bool TryPlaceItem(ItemSlot slot)
        {
            for (int x = 0; x < _size.x; x++)
            {
                for (int y = 0; y < _size.y; y++)
                {
                    Cell checkCell = GetCell(x, y);
                    if (SlotCanFit(slot, checkCell))
                    {
                        slot.SetPosition(new Vector2Int(x, y));

                        print("Placing" + slot.ItemData.name + " slot at " + x + " " + y + " it used this number of cells " + GetSlotCells(slot, checkCell).ToList().Count);

                        foreach (var cell in GetSlotCells(slot, checkCell))
                        {
                            print(cell.Position);
                            cell.SetSlot(slot);
                        }
                        _allItems.Add(slot);
                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return false;
        }

        public bool TryPlaceItem(ItemSlot slot, Cell origin)
        {
            if (SlotCanFit(slot, origin))
            {
                slot.SetPosition(origin.Position);

                foreach (var cell in GetSlotCells(slot, origin))
                {
                    cell.SetSlot(slot);
                }
                return true;
            }

            return false;
        }

        public bool SlotCanFit(ItemSlot slot, Cell origin)
        {
            int checkSizeX = slot.Size.x;
            int checkSizeY = slot.Size.y;

            float checkFloatX = (float)checkSizeX / 2f;
            float checkFloatY = (float)checkSizeY / 2f;

            // Check Horizontal Cells To the left
            for (int x = 1; x <= Mathf.FloorToInt(checkFloatX); x++)
            {
                Cell checkCell = GetCell(origin.Position.x - x, origin.Position.y);
                if (checkCell == null || checkCell.InUse) return false;
            }

            // Check Horizontal Cells to the right
            for (int y = 1; y <= Mathf.CeilToInt(checkFloatX); y++)
            {
                Cell checkCell = GetCell(origin.Position.x + y, origin.Position.y);
                if (checkCell == null || checkCell.InUse) return false;
            }

            // Check Vertical Cells to the bottom
            for (int u = 1; u <= Mathf.FloorToInt(checkFloatY); u++)
            {
                Cell checkCell = GetCell(origin.Position.x, origin.Position.y - u);
                if (checkCell == null || checkCell.InUse) return false;
            }

            // Check Vertical Cells to the top
            for (int v = 1; v <= Mathf.CeilToInt(checkFloatY); v++)
            {
                Cell checkCell = GetCell(origin.Position.x, origin.Position.y + v);
                if (checkCell == null || checkCell.InUse) return false;
            }

            return true;
        }

        public IEnumerable<Cell> GetSlotCells(ItemSlot slot, Cell origin)
        {
            int checkSizeX = slot.Size.x;
            int checkSizeY = slot.Size.y;

            if (checkSizeX == 0 && checkSizeY == 0)
            {
                yield return origin;
                yield break;
            }

            float checkFloatX = (float)checkSizeX / 2f;
            float checkFloatY = (float)checkSizeY / 2f;

            // Check Horizontal Cells To the left
            for (int i = 1; i <= Mathf.FloorToInt(checkFloatX); i++)
            {
                Cell checkCell = GetCell(origin.Position.x - i, origin.Position.y);
                yield return checkCell;
            }

            // Check Horizontal Cells to the right
            for (int i = 1; i <= Mathf.CeilToInt(checkFloatX); i++)
            {
                Cell checkCell = GetCell(origin.Position.x + i, origin.Position.y);
                yield return checkCell;
            }

            // Check Vertical Cells to the bottom
            for (int i = 1; i <= Mathf.FloorToInt(checkFloatY); i++)
            {
                Cell checkCell = GetCell(origin.Position.x, origin.Position.y - i);
                yield return checkCell;
            }

            // Check Vertical Cells to the top
            for (int i = 1; i <= Mathf.CeilToInt(checkFloatY); i++)
            {
                Cell checkCell = GetCell(origin.Position.x, origin.Position.y + i);
                yield return checkCell;
            }
        }

        public Cell GetCell(int x, int y)
        {
            
            if (_cells.GetLength(0) <= x || _cells.GetLength(1) <= y || x < 0 || y < 0) return null;

            return _cells[x, y];
        }
    }
}