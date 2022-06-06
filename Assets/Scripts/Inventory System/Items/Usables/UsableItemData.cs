using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    [CreateAssetMenu(fileName = "Usable Item Data", menuName = "Items/New Usable Item Data")]
    public class UsableItemData : GameItemData<UsableItem>
    {
        public override ItemTypes GetItemType()
        {
            return ItemTypes.Consumable;
        }
    }
}