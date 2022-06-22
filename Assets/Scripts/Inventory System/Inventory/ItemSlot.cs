using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SpaceHorror.UI;

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
        public ItemSlot(GameItemData data, Inventory parent, int count)
        {
            _itemData = data;
            _itemCount = count;
            _itemParameters = null;
            _parentInventory = parent;
        }
        public ItemSlot(GameItemData data, Inventory parent, int count, object parameters)
        {
            _itemData = data;
            _itemCount = count;
            _itemParameters = parameters;
            _parentInventory = parent;
        }
        public ItemSlot(ItemSlot slot, Inventory parent)
        {
            _itemData = slot.ItemData;
            _itemCount = slot.ItemCount;
            _itemParameters = slot.ItemParameters;
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
                return _itemData.Equals(slot.ItemData) && Position == slot.Position;
            }
            else    
            {
                return _itemParameters.Equals(slot.ItemParameters) && _itemData.Equals(slot.ItemData) && Position == slot.Position;
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
        [InventoryButton("Drop")]
        public void Drop()
        {
            if(_itemCount == 1)
            {
                DropItem(1);
            }
            else
            {
                IntActionWindow window = MonoBehaviour.FindObjectOfType<IntActionWindow>();
                window.Init(DropItem, _itemCount);
            }
        }

        [InventoryButton("Consume", typeof(ConsumableItemData))]
        public void Use()
        {
            Debug.Log("Consumed " + _itemData.name);
        }
        #endregion

        #region Inventory Methods

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
            if(cell.Slot != null && TryMergeSlot(cell.Slot, out bool stillActive))
            {
                return stillActive;
            }
            else
            {
                if(cell.Parent == ParentInventory)
                {
                    return _parentInventory.TryPlaceItem(this, cell);
                }
                else
                {
                    if(cell.Parent.TryPlaceItem(new ItemSlot(this, cell.Parent), cell))
                    {
                        DestroySlot();
                        return true;
                    }
                    return false;
                }
            }
        }

        public void DropBack()
        {
            _parentInventory.TryPlaceItem(this, _parentInventory.GetCell(Position.x, Position.y));
        }

        /// <summary>
        /// Returns true if the slot was destroyed.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public bool TryMergeSlot(ItemSlot slot, out bool died)
        {
            died = false;

            if (_itemData.Stackable == false || slot.ItemData != _itemData) return false;
            if (EqualParameters(slot) == false) return false;

            if(slot.ItemCount + _itemCount > _itemData.StackSize)
            {
                int toRemove = _itemCount - ((slot.ItemCount + _itemCount) - _itemData.StackSize);

                slot.AddToCount(toRemove);
                RemoveFromCount(toRemove);

                died = false;

                return true;
            }

            died = true;

            slot.AddToCount(_itemCount);

            DestroySlot();

            return true;
        }

        private void DestroySlot()
        {
            onDestroy?.Invoke();
            _parentInventory.RemoveSlot(this);
        }

        private void DropItem(int amount)
        {
            if (amount <= 0) return;

            if (amount > _itemCount)
            {
                amount = _itemCount;
            }

            GameItemDrop drop = MonoBehaviour.Instantiate(_itemData.Drop, _parentInventory.DropPoint, Quaternion.identity);

            drop.Init(this, amount);

            if (amount == _itemCount)
            {
                DestroySlot();
            }
            else
            {
                RemoveFromCount(amount);
            }
        }

        private bool EqualParameters(ItemSlot slot)
        {
            if (slot.ItemParameters == null && _itemParameters == null) return true;
            if (slot.ItemParameters != null && _itemParameters == null) return false;
            if (slot.ItemParameters == null && _itemParameters != null) return false;

            return _itemParameters.Equals(slot.ItemParameters);
        }

        #endregion
    }
}

