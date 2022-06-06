using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            AddSlot(slot);
        }
        public void AddItem(GameItemData itemData)
        {
            InventorySlot slot = new InventorySlot(itemData);
            AddSlot(slot);
        }

        private void AddSlot(InventorySlot slot)
        {
            InventorySlot existingSlot = _slots.Find(x => x.Equals(slot));
            if(existingSlot != null)
            {
                existingSlot.AddToCount(slot.ItemCount);
            }
            else
            {
                _slots.Add(slot);
            }
        }

        private void Start()
        {
            _slots = new List<InventorySlot>(_slots.Distinct());
        }

        private void OnValidate()
        {
            if(new List<InventorySlot>(_slots.Distinct()).Count != _slots.Count)
            {
                Debug.LogWarning("Warning : Inventory " + _name + " may contain duplicate slots, these will be destroyed at runtime");
            }
        }
    }
}