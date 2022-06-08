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
        private InventoryCell[,] _slots;

        public string Name { get => _name; }
        public InventoryCell[,] InventorySlots { get => _slots; }
        public IEnumerable<ItemSlot> AllItems { get => new List<ItemSlot>(_allItems); }


        public event InventorySlotHandler onSlotAdded;

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
            _slots = new InventoryCell[_size.x, _size.y];
            for (int x = 0; x < _size.x; x++)
            {
                for (int y = 0; y < _size.y; y++)
                {
                    _slots[x,y] = new InventoryCell(x, y);
                }
            }
        }

        public void TryPlaceItem(ItemSlot slot)
        {

        }
    }
}