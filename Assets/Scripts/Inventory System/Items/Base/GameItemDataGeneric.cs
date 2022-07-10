using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    [CreateAssetMenu(fileName = "Generic Item Data", menuName = "Items/New Generic Item Data")]
    public class GameItemData<T> : GameItemData where T: GameItem
    {
        protected virtual void OnValidate()
        {
            if(_drop is T == false)
            {
                _drop = null;
            }
        }
    }
}