using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    [System.Serializable]
    public class ConsumableSlot
    {
        [SerializeField] private ItemSlot _activeConsumable;


        public ItemSlot ActiveSlot { get => _activeConsumable; }


        public void SetSlot(ItemSlot slot)
        {
            _activeConsumable = slot;
        }

        public void Consume()
        {
            _activeConsumable.Use();
        }
    }
}
