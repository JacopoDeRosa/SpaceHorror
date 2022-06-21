using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Items/New Weapon Data")]
    public class EquippableItemData : GameItemData
    {
        [SerializeField][InspectorField("Attack Rate")]
        private float _fireRate;

        public override ItemTypes GetItemType()
        {
            return ItemTypes.Equipment;
        }
    }
}