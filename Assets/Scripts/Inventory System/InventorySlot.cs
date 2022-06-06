using UnityEngine;

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
            

        public override bool Equals(object obj)
        {
            InventorySlot slot = obj as InventorySlot;

            if (slot == null) return false;

            return _itemParameters.Equals(slot.ItemParameters) && _itemData.Equals(slot.ItemData);
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

    }
}

