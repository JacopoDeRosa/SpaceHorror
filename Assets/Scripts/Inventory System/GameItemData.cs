using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class GameItemData : ScriptableObject
    {
        [SerializeField] private float _weight;

        public float Weight { get => _weight; }
    }
}