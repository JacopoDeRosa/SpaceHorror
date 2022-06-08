using System.Collections;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class Cell
    {
        private ItemSlot _slot;
        private Vector2Int _position;

        public ItemSlot Occupied { get => _slot; }
        public Vector2Int Position { get => _position; }

        public bool InUse { get => _slot != null; }

        public Cell()
        {
            _slot = null;
        }

        public Cell(int x, int y)
        {
            _position = new Vector2Int(x, y);
        }

        public void SetSlot(ItemSlot slot)
        {
            _slot = slot;
        }

        
    }
}