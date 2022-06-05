using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    [System.Serializable]
    public class InventorySlot
    {
        [SerializeField] private GameItemData _slotItem;
        [SerializeField] private int _itemNumber;

        private object _itemParameters;

        public GameItemData SlotItem { get => _slotItem; }
    }
}

