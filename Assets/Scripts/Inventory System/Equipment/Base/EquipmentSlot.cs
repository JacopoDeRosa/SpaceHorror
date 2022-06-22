using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    [System.Serializable]
    public class EquipmentSlot
    {
        [SerializeField] private EquippableItem _item;

        private Equipment _parent;

        public EquippableItem Item { get => _item; }
        public Equipment Parent { get => _parent; }

        public EquipmentSlot(Equipment parent)
        {
            _parent = parent;
        }

        public void Equip()
        {

        }
        public void UnEquip()
        {

        }

        public void SetItem(EquippableItem item)
        {
            _item = item;
        }

        public EquippableItem RemoveItem()
        {
            UnEquip();
            return _item;
        }

    }
}