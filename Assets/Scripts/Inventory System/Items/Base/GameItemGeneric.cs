using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class GameItem<T> : GameItem where T: GameItemData
    {
        [SerializeField]
        new protected T  _data;

        new public T Data { get => _data; }
    }
}