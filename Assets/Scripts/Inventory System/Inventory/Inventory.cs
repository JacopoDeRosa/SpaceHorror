using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SpaceHorror.InventorySystem
{
    [AddComponentMenu("Spacehorror/Inventory")]
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private Vector2Int _size;
        [SerializeField] private List<ItemSlot> _initialItems;
        [SerializeField] private Vector3 _dropPoint;
        

        
        private List<ItemSlot> _allItems;
        private Cell[,] _cells;

        public string Name { get => _name; }
        public Cell[,] InventoryCells { get => _cells; }
        public Vector2Int Size { get => _size; }
        public List<ItemSlot> AllItems { get => new List<ItemSlot>(_allItems); }

        public event ItemSlotHandler onSlotAdded;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawSphere(transform.TransformPoint(_dropPoint), 0.15f);
        }
#endif

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
                    Cell newCell = new Cell(x, y);
                    newCell.SetParent(this);
                    _cells[x,y] = newCell;
                }
            }

            foreach (ItemSlot item in _initialItems)
            {
                item.SetParentInventory(this);
                TryPlaceItem(item);
            }
        }

        /// <summary>
        /// Tries to place an item in an optimal position.
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
                        PlaceItemAtPoint(slot, checkCell);
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
                PlaceItemAtPoint(slot, targetCell);
                return true;
            }

            return false;
        }

        private void PlaceItemAtPoint(ItemSlot slot, Cell point)
        {
            slot.SetPosition(point.Position);

            foreach (var cell in GetSlotCells(slot, point))
            {
                cell.SetSlot(slot);
            }

            if (_allItems.Contains(slot) == false)
            {
                _allItems.Add(slot);
                onSlotAdded?.Invoke(slot);
            }   

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

        public void RemoveSlot(ItemSlot slot)
        {
            if (slot.ParentInventory != this) return;
            ItemSlot mSlot = _allItems.Find(x => x.Equals(slot));
            if (mSlot != null)
            {
                _allItems.Remove(mSlot);
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

        private IEnumerable<ItemSlot> GetAllSlotsWithSameItem(ItemSlot slot)
        {
            foreach  (ItemSlot mSlot in _allItems)
            {
                if(mSlot.ItemData == slot.ItemData)
                {
                    yield return mSlot;
                }
            }
        }
    }
}