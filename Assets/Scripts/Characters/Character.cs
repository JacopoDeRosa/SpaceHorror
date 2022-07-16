using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceHorror.InventorySystem;

namespace SpaceHorror
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private Inventory _inventory;

        /// <summary>
        /// The Name of the character, not to be confused with gameobject.name
        /// </summary>
        public string Name { get => _name; }
        public Inventory Inventory { get => _inventory; }
    }
}
