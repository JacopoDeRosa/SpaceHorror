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
        private Type _dataType;

        public string Text { get => _text; }
        public Type DataType { get => _dataType; }
        public InventoryButton(string buttonText)
        {
            _text = buttonText;
            _dataType = typeof(GameItemData);
        }
        public InventoryButton(string buttonText, Type dataType)
        {
            _text = buttonText;
            _dataType = dataType;
        }
    }
}