using System.Collections;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class InventoryCell : MonoBehaviour
    {
        private ItemSlot _slot;
        private Vector2Int _position;

        public ItemSlot Occupied { get => _slot; }
        public Vector2 Position { get => _position; }

        public bool InUse { get => _slot != null; }

        public InventoryCell()
        {
            _slot = null;
        }

        public InventoryCell(int x, int y)
        {
            _position = new Vector2Int(x, y);
        }

        public void SetSlot(ItemSlot slot)
        {
            _slot = slot;
        }

        
    }
}