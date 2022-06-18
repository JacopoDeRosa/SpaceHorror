using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class GameItemDrop : MonoBehaviour
    {
        [SerializeField]
        private GameItemData _itemData;      
        [SerializeField]
        private int _itemCount;

        private object _itemParams;

        public GameItemData ItemData { get => _itemData; }
        public int ItemCount { get => _itemCount; }
        public object ItemParameters { get => _itemParams; }


        public void Init(ItemSlot slot, int count)
        {
            _itemData = slot.ItemData;
            _itemParams = slot.ItemParameters;
            _itemCount = count;
            OnInit(_itemParams);
        }

        protected virtual void OnInit(object parameters)
        {

        }       
    }
}
