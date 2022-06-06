using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceHorror.InventorySystem.UI
{
    public class SlotOptionsMenu : MonoBehaviour, IPointerMoveHandler
    {
       
        [SerializeField] private SlotOptionsButton[] _buttons;

        private Queue<SlotOptionsButton> _freeButtons;
        private bool _visible;

        private void Start()
        {
            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
                button.onPress += ToggleVisible;
            }
        }

        public void SetVisible(bool visible)
        {
            if (visible == _visible) return;

            if (visible == false)
            {
                foreach (var button in _buttons)
                {
                    button.gameObject.SetActive(false);
                    _freeButtons = new Queue<SlotOptionsButton>(_buttons);
                }
            }
            else
            {
                foreach (var button in _buttons)
                {
                    button.gameObject.SetActive(true);
                }
            }

            _visible = visible;
        }
        public void ToggleVisible()
        {
            SetVisible(!_visible);
        }
        public void OnPointerMove(PointerEventData eventData)
        {
            if (_visible == false) return;

            if(RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, eventData.position) == false)
            {
                SetVisible(false);
            }
        }
    }
}
