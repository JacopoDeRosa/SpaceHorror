using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class GameItemDrop : MonoBehaviour
    {
        private GameItemData _itemData;
        private object _itemParams;
        private int _itemCount;



        public void Init(ItemSlot slot)
        {
            _itemData = slot.ItemData;
            _itemParams = slot.ItemParameters;
            _itemCount = slot.ItemCount;
            OnInit(_itemParams);
        }

        protected virtual void OnInit(object parameters)
        {

        }       
    }
}
