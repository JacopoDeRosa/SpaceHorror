using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    [System.Serializable]
    public class EquippableSlot
    {
        [SerializeField] private EquippableItem _item;

        public EquippableItem Item { get => _item; }

        public void SetItem(EquippableItem item)
        {
            _item = item;
        }
    }
}