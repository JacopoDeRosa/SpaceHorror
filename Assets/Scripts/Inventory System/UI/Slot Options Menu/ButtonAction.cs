using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace SpaceHorror.InventorySystem
{
    // Stores an action for a button with its relative name.
    public class ButtonAction
    {
        private string _name;
        private Action _action;

        public string Name { get => _name; }
        public Action Action { get => _action; }

        public ButtonAction(string name, Action action)
        {
            _name = name;
            _action = action;          
        }

    }
}
