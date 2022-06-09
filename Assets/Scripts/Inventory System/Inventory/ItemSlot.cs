using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SpaceHorror.InventorySystem
{
    [System.Serializable]
    public class ItemSlot
    {
        [SerializeField] private GameItemData _itemData;
        [SerializeField] private int _itemCount;

        private object _itemParameters;

        private Vector2Int _position;

        public object ItemParameters { get => _itemParameters; }
        public GameItemData ItemData { get => _itemData; }
        public int ItemCount { get => _itemCount; }
        public Vector2Int Size { get => _itemData.Size; }
        public Vector2Int Position { get => _position; }

        public event ItemSlotHandler onSlotChanged;

        #region Constructors
        public ItemSlot(GameItem item)
        {
            _itemParameters = item.PackParameters();
            _itemData = item.Data;
            _itemCount = 1;
        }
        public ItemSlot(GameItemData data)
        {
            _itemData = data;
            _itemCount = 1;
            _itemParameters = null;
        }
        #endregion

        #region Equality Comparisons
        public override bool Equals(object obj)
        {
            ItemSlot slot = obj as ItemSlot;

            if (slot == null) return false;

            if(ItemParameters == null)
            {
                return _itemData.Equals(slot.ItemData);
            }
            else    
            {
                return _itemParameters.Equals(slot.ItemParameters) && _itemData.Equals(slot.ItemData);
            }        
        }
        public override int GetHashCode()
        {
            if (_itemParameters == null)
            {
              return _itemData.GetHashCode();
            }
            else
            {
              return _itemData.GetHashCode() + _itemParameters.GetHashCode(); 
            }
        }

        public static bool operator ==(ItemSlot a, ItemSlot b)
        {
            if(ReferenceEquals(a, b))
            {
                return true;
            }
            if(ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }
        public static bool operator !=(ItemSlot a, ItemSlot b)
        {
            return !(a == b);
        }
        #endregion

        #region Options
        [InventoryButton("Drop x1")]
        private void DropOne()
        {
            Debug.Log("Dropped 1 of " + _itemData.name);
        }

        [InventoryButton("Drop x5")]
        private void DropFive()
        {
            Debug.Log("Dropped 5 of " + _itemData.name);
        }

        [InventoryButton("Consume", typeof(UsableItemData))]
        private void Use()
        {
            Debug.Log("Consumed " + _itemData.name);
        }
        #endregion

        public void AddToCount(int amount)
        {
            _itemCount += amount;
            InvokeSlotChanged();
        }
        
        public void RemoveFromCount(int amount)
        {
            _itemCount -= amount;
            InvokeSlotChanged();
        }

        public void SetPosition(Vector2Int position)
        {
            _position = position;
        }

        private void InvokeSlotChanged()
        {
            onSlotChanged?.Invoke(this);
        }

        private void ResetEvents()
        {
            onSlotChanged = null;
        }

    }
}
