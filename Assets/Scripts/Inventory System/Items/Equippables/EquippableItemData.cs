using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Items/New Weapon Data")]
    public class EquippableItemData : GameItemData
    {
        [SerializeField] private EquippableItem _item;
        [SerializeField] private AnimatorOverrideController _animatorOverride;

        public EquippableItem Item { get => _item; }
        public AnimatorOverrideController AnimatorOverride { get => _animatorOverride; }

        public override ItemTypes GetItemType()
        {
            return ItemTypes.Equipment;
        }

        private void OnValidate()
        {
            if (_item != null)
            {
                if (_item.Data != this)
                {
                    Debug.Log("The currently set item for " + name + " is not the same the Item's Data, is this Correct?");
                }
            }
        }
    }
}