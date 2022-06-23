using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class GameItem : MonoBehaviour
    {
        [SerializeField]
        protected GameItemData _data;

        public GameItemData Data { get => _data; }

        public virtual object PackParameters()
        {
            return null;
        }
        public virtual void LoadParameters(object parameters)
        {

        }
    }
}