using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SpaceHorror.InventorySystem
{
    [System.Serializable]
    public class InventorySlot
    {
        [SerializeField] private GameItemData _itemData;
        [SerializeField] private int _itemCount;

        private object _itemParameters;

        public object ItemParameters { get => _itemParameters; }
        public GameItemData ItemData { get => _itemData; }
        public int ItemCount { get => _itemCount; }

        #region Constructors
        public InventorySlot(GameItem item)
        {
            _itemParameters = item.PackParameters();
            _itemData = item.Data;
            _itemCount = 1;
        }
        public InventorySlot(GameItemData data)
        {
            _itemData = data;
            _itemCount = 1;
            _itemParameters = null;
        }
        #endregion

        #region Equality Comparisons
        public override bool Equals(object obj)
        {
            InventorySlot slot = obj as InventorySlot;

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

        public static bool operator ==(InventorySlot a, InventorySlot b)
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
        public static bool operator !=(InventorySlot a, InventorySlot b)
        {
            return !(a == b);
        }
        #endregion

        #region Options

        public IEnumerable<ButtonAction> GetButtonActions()
        {
            yield return new ButtonAction("Drop x1", DropOne);
            yield return new ButtonAction("Drop x5", DropFive);

            UsableItemData usableItemData = _itemData as UsableItemData;
            if(usableItemData != null)
            {
                yield return new ButtonAction("Consume", Use);
            }
            
        }

        private void DropOne()
        {

        }

        private void DropFive()
        {

        }

        private void Use()
        {

        }
        #endregion

        public void AddToCount(int amount)
        {
            _itemCount += amount;
        }
        
        public void RemoveFromCount(int amount)
        {
            _itemCount -= amount;
        }
    }
}

