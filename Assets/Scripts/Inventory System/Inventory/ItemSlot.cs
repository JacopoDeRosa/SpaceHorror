using UnityEngine;
using System;
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
        private Inventory _parentInventory;

        public object ItemParameters { get => _itemParameters; }
        public GameItemData ItemData { get => _itemData; }
        public int ItemCount { get => _itemCount; }   
        public Vector2Int Size { get => _itemData.Size; }
        public Vector2Int Position { get => _position; }
        public Inventory ParentInventory { get => _parentInventory; }

        public event ItemSlotHandler onSlotPositionChange;
        public event ItemSlotHandler onSlotCountChange;
        public event Action onDestroy;

        #region Constructors
        public ItemSlot(GameItem item, Inventory parent)
        {
            _itemParameters = item.PackParameters();
            _itemData = item.Data;
            _itemCount = 1;
            _parentInventory = parent;
        }
        public ItemSlot(GameItemData data, Inventory parent)
        {
            _itemData = data;
            _itemCount = 1;
            _itemParameters = null;
            _parentInventory = parent;
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
            onSlotCountChange?.Invoke(this);
        }
        
        public void RemoveFromCount(int amount)
        {
            _itemCount -= amount;
            onSlotCountChange?.Invoke(this);
        }

        public void SetPosition(Vector2Int position)
        {
            _position = position;
            onSlotPositionChange?.Invoke(this);
        }

        private void ResetPositionChangeEvent()
        {
            onSlotPositionChange = null;
        }

        public void SetParentInventory(Inventory inventory)
        {
            _parentInventory = inventory;
        }

        public void LiftFromInventory()
        {
            _parentInventory.ClearItemCells(this);
        }

        public bool DropInInventory(Cell cell)
        {
            if(cell.Slot != null)
            {
               return TryMergeSlot(cell.Slot);
            }
            else
            {
                return _parentInventory.TryPlaceItem(this, cell);
            }
        }

        public void DropBack()
        {
            _parentInventory.TryPlaceItem(this, _parentInventory.GetCell(Position.x, Position.y));
        }

        private bool TryMergeSlot(ItemSlot slot)
        {
            if (_itemData.Stackable == false || slot.ItemData != _itemData || slot.ParentInventory != ParentInventory) return false;

            slot.AddToCount(_itemCount);
            DestorySlot();

            return true;
        }

        private void DestorySlot()
        {
            onDestroy?.Invoke();
        }
    }
}

