using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SpaceHorror.InventorySystem
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class InventoryButton : Attribute
    {
        private string _text;

        public string Text { get => _text; }

        public InventoryButton(string buttonText)
        {
            _text = buttonText;
        }
    }
}