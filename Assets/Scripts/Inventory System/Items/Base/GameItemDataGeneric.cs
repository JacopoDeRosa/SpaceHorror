using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    [CreateAssetMenu(fileName = "Generic Item Data", menuName = "Items/New Generic Item Data")]
    public class GameItemData<T> : GameItemData where T: GameItem
    {
        new protected T _pickUp;

        new public T PickUp { get => _pickUp; }
    }
}