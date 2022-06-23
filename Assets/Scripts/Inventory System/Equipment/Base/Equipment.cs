using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class Equipment : MonoBehaviour
    {
        [SerializeField] private EquippableSlot _equippableA, _equippableB;
        [SerializeField] private ConsumableSlot _consumableA, _consumableB;
        [SerializeField] private Inventory _parentInventory;
        [SerializeField] private Transform _itemContainer;

        public Inventory ParentInventory { get => _parentInventory; }

        public bool TrySetEquippableItem(ItemSlot itemSlot, EquipmentSlotType type)
        {
            if(type == EquipmentSlotType.Primary)
            {
                return TrySetEquippableItem(itemSlot, _equippableA);
            }
            else if(type == EquipmentSlotType.Secondary)
            {
                return TrySetEquippableItem(itemSlot, _equippableB);
            }

            return false;
        }

        public bool TrySetEquippableItem(ItemSlot itemSlot, EquippableSlot slot)
        {
            if (slot.Item != null) return false;

            EquippableItemData equippableItemData = itemSlot.ItemData as EquippableItemData;

            if (equippableItemData == null) return false;

            EquippableItem item = Instantiate(equippableItemData.Item, _itemContainer);

            item.LoadParameters(itemSlot.ItemParameters);

            slot.SetItem(item);

            return true;
        }

        public bool TrySetConsumableItem(ItemSlot itemSlot, EquipmentSlotType type)
        {
            if (type == EquipmentSlotType.Primary)
            {
                return TrySetConsumableItem(itemSlot, _consumableA);
            }
            else if (type == EquipmentSlotType.Secondary)
            {
                return TrySetConsumableItem(itemSlot, _consumableB);
            }

            return false;
        }

        public bool TrySetConsumableItem(ItemSlot itemSlot, ConsumableSlot slot)
        {
            if (slot.ActiveSlot.ItemData != null) return false;

            if(itemSlot.ItemData is ConsumableItemData)
            {
                slot.SetSlot(itemSlot);        
            }
            return true;
        }

        public void ActivateSlot(EquipmentSlotType slotType)
        {

        }

        public ItemSlot ClearEquippableSlot(EquipmentSlotType equipmentSlotType)
        {
            if(equipmentSlotType == EquipmentSlotType.Primary)
            {
                return ClearEquippableSlot(_equippableA);
            }
            else if(equipmentSlotType == EquipmentSlotType.Secondary)
            {
                return ClearEquippableSlot(_equippableB);
            }

            return null;
        }

        private ItemSlot ClearEquippableSlot(EquippableSlot slot)
        {
            if (slot.Item == null) return null;

            ItemSlot itemSlot = new ItemSlot(slot.Item, null);

            Destroy(slot.Item.gameObject);

            slot.RemoveItem();

            return itemSlot;
        }

        public ItemSlot ClearConsumableSlot(EquipmentSlotType equipmentSlotType)
        {
            if (equipmentSlotType == EquipmentSlotType.Primary)
            {
                return ClearConsumableSlot(_consumableA);
            }
            else if (equipmentSlotType == EquipmentSlotType.Secondary)
            {
                return ClearConsumableSlot(_consumableB);
            }

            return null;
        }

        public ItemSlot ClearConsumableSlot(ConsumableSlot slot)
        {
            if (slot.ActiveSlot.ItemData == null) return null;

            ItemSlot itemSlot = slot.ActiveSlot;

            slot.ClearActiveSlot();

            return itemSlot;
        }
    }
}
