using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SpaceHorror.InventorySystem
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InspectorField : Attribute
    {
        // TODO: Implement Order when inspector progresses

        private string _name;
        private int _order;

        public string Name { get => _name; }
        public int Order { get => _order; }

        public InspectorField(string name)
        {
            _name = name;
        }

        public InspectorField(string name, int order)
        {
            _name = name;
            _order = order;
        }
    }
}
