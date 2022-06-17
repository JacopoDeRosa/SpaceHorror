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

        [SerializeField]
        [HideInInspector]
        private bool _stackable;

        [SerializeField]
        [HideInInspector]
        private int _maxStackSize;

        [SerializeField]
        private Vector2Int _size;

        [SerializeField]
        protected GameItemDrop _drop;

        public float Weight { get => _weight; }

        public Sprite Icon { get => _icon; }

        public string Description { get => _description; }

        public Vector2Int Size { get => _size; }

        public bool Stackable { get => _stackable; }

        public int StackSize { get => _maxStackSize; }

        public GameItemDrop Drop { get => _drop; }


        public virtual ItemTypes GetItemType()
        {
            return ItemTypes.Generic;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Editor Only Function, do not call in game
        /// </summary>
        /// <param name="stackable"></param>
        public void SetStackable(bool stackable)
        {
            _stackable = stackable;
        }

        public void SetStackSize(int size)
        {
            _maxStackSize = size;
        }
#endif
    }
}