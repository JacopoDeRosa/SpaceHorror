using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class GameItem<T> : GameItem where T: GameItemData
    {
        new protected T  _data;

        new public T Data { get => _data; }

        public virtual object PackParameters()
        {
            return null;
        }

        public virtual void LoadParameters()
        {

        }
    }
}