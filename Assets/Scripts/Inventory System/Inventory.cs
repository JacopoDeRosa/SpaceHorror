using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<InventorySlot> _slots;

        public float GetTotalWeight()
        {
            float weight = 0;
            foreach (InventorySlot slot in _slots)
            {
                weight += slot.SlotItem.Weight;
            }
            return weight;
        }

    }
}