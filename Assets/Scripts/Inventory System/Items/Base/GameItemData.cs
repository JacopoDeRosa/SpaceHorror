using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem
{
    [CreateAssetMenu(fileName = "Generic Item Data", menuName = "Items/New Generic Item Data")]
    public class GameItemData : ScriptableObject
    {
        [SerializeField]
        [InspectorField("Weight")]
        protected float _weight;

        [SerializeField]
        private Sprite _icon;

        [SerializeField]
        [TextArea]
        private string _description;

        protected GameItem _pickUp;

        public float Weight { get => _weight; }
        public Sprite Icon { get => _icon; }
        public string Description { get => _description; }
        public GameItem PickUp { get => _pickUp; }

        public virtual ItemTypes GetItemType()
        {
            return ItemTypes.Generic;
        }
    }
}