using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class EquippableItem : GameItem
    {
        // TODO: Add a character class to the equippable item
        private Character _user;

        protected Character User { get => _user; }

        public virtual void PrimaryUse()
        {

        }

        public virtual void SecondaryUse()
        {

        }

        public virtual void UtilityUse()
        {

        }

        public void SetUser(Character user)
        {
            _user = user;
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
