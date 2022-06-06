using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class GameItemData<T> : GameItemData where T: GameItem
    {
        [SerializeField]
        new protected T _pickUp;

        new public T PickUp { get => _pickUp; }
    }
}