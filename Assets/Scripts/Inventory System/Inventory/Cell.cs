using System.Collections;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class Cell
    {
        private Vector2Int _position;
        private Inventory _parent;



        public ItemSlot Slot { get; private set; }
        public Vector2Int Position { get => _position; }
        public Inventory Parent { get => _parent; }

        public CellHandler onCellUpdated;

        public bool InUse { get => Slot != null; }

        public Cell()
        {
            Slot = null;
        }

        public Cell(int x, int y)
        {
            _position = new Vector2Int(x, y);
        }

        public void SetSlot(ItemSlot slot)
        {
            Slot = slot;
            CallOnCellUpdated();
        }

        public void SetParent(Inventory parent)
        {
            _parent = parent;
        }

        private void CallOnCellUpdated()
        {
            onCellUpdated?.Invoke(this);
        }



        
    }
}