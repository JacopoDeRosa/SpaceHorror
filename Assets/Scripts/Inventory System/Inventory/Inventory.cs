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
        [SerializeField] private List<InventorySlot> _initialItems;


        private InventorySlot[,] _slots;
        public string Name { get => _name; }
        public InventorySlot[,] InventorySlots { get => _slots; }


        public event InventorySlotHandler onSlotAdded;

        public float GetTotalWeight()
        {
            float weight = 0;
            foreach (InventorySlot slot in _slots)
            {
                weight += slot.ItemData.Weight;
            }
            return weight;
        }
    }
}