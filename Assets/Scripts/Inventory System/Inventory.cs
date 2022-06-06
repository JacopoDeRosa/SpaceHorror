using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private List<InventorySlot> _slots;

        public string Name { get => _name; }
        public List<InventorySlot> InventorySlots { get => new List<InventorySlot>(_slots); }


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
        public void AddItem(GameItem item)
        {
            InventorySlot slot = new InventorySlot(item);
        }
    }
}