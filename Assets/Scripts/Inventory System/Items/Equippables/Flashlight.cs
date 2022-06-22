using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    public class Flashlight : EquippableItem
    {
        [SerializeField] private Light _light;
        
        private bool _on;

        public override void PrimaryUse()
        {
            _on = !_on;

            if(_on)
            {
                _light.enabled = true;
            }
            else
            {
                _light.enabled = false;
            }
        }
    }
}
