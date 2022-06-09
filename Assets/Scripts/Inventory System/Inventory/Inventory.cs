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

        protected virtual void Awake()
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
                item.SetParentInventory(this);
                if (TryPlaceItem(item) == false) break;
            }
        }

        /// <summary>
        /// Places an item in an optimal position.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
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

                        foreach (var cell in GetSlotCells(slot, checkCell))
                        {
                            cell.SetSlot(slot);
                        }
                        _allItems.Add(slot);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Tries to place an item at a cell.
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="targetCell"></param>
        /// <returns></returns>
        public bool TryPlaceItem(ItemSlot slot, Cell targetCell)
        {
            if (SlotCanFit(slot, targetCell))
            {
                slot.SetPosition(targetCell.Position);

                foreach (var cell in GetSlotCells(slot, targetCell))
                {
                    cell.SetSlot(slot);
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the cells occupied by a slot to empty.
        /// </summary>
        /// <param name="slot"></param>
        public void ClearItemCells(ItemSlot slot)
        {
            if (slot.ParentInventory != this) return;

            foreach (Cell cell in GetSlotCells(slot, GetCell(slot.Position.x, slot.Position.y)))
            {
                cell.SetSlot(null);
            }
        }

        /// <summary>
        /// Checks if a slot can fit on a cell.
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public bool SlotCanFit(ItemSlot slot, Cell origin)
        {
            Vector2 offsetF = slot.Size / 2;
            Vector2Int offset = new Vector2Int(-Mathf.FloorToInt(offsetF.x), Mathf.FloorToInt(offsetF.y));
            for (int y = 0; y < slot.Size.y; y++)
            {
                for (int x = 0; x < slot.Size.x; x++)
                {
                    Vector2Int checkInt = origin.Position - offset;
                    Cell checkCell = GetCell(checkInt.x, checkInt.y);
                    if (checkCell == null || checkCell.InUse) return false;
                    offset.x++;
                }
                offset.x = -Mathf.FloorToInt(offsetF.x);
                offset.y--;
            }
         
          
            return true;
        }

        /// <summary>
        /// Gets all cells a slot would use if placed at a position, some cells might be null if position is invalid.
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public IEnumerable<Cell> GetSlotCells(ItemSlot slot, Cell origin)
        {
           
            Vector2 offsetF = slot.Size / 2;
            Vector2Int offset = new Vector2Int(-Mathf.FloorToInt(offsetF.x), Mathf.FloorToInt(offsetF.y));
            for (int y = 0; y < slot.Size.y; y++)
            {
                for (int x = 0; x < slot.Size.x; x++)
                {
                    Vector2Int checkInt = origin.Position - offset;
                    Cell checkCell = GetCell(checkInt.x, checkInt.y);
                    yield return checkCell;
                    offset.x++;
                }
                offset.x = -Mathf.FloorToInt(offsetF.x);
                offset.y--;
            }
        }

        /// <summary>
        /// Gets a cell at a position, return null if out of bounds.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell GetCell(int x, int y)
        {
            
            if (_cells.GetLength(0) <= x || _cells.GetLength(1) <= y || x < 0 || y < 0) return null;

            return _cells[x, y];
        }

        /// <summary>
        /// Gets a cell at a position, return null if out of bounds.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell GetCell(Vector2Int position)
        {
            return GetCell(position.x, position.y);
        }
    }
}