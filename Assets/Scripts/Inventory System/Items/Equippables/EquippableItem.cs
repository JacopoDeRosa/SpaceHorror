using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class EquippableItem : GameItem
    {
        public virtual void PrimaryUse()
        {

        }

        public virtual void SecondaryUse()
        {

        }

        public virtual void UtilityUse()
        {

        }

        protected virtual void OnValidate()
        {
            if(_data != null && _data is EquippableItemData == false)
            {
                _data = null;
                Debug.Log("Equippable items only accept EquippableItemData");
            }
        }
    }
}
