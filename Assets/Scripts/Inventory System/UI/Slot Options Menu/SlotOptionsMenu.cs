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


        private void Awake()
        {
            ResetButtons();
        }

        private void ResetButtons()
        {
            _freeButtons = new Queue<SlotOptionsButton>(_buttons);

            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);

                // Setting the event to null clears it of all trash delegates.
                button.onPress = null;

                // Re-Adds the toggle visible action.
                button.onPress += ToggleVisible;

                button.SetInUse(false);
            }
        }

        public void InitButtons(IEnumerable<ButtonAction> actions)
        {
            foreach (ButtonAction action in actions)
            {
                if (_freeButtons.Count == 0) return;

                // Gets the button from the queue
                var button = _freeButtons.Dequeue();

                // Setting the event to null clears it of all trash delegates.
                button.onPress = null;

                // Re-Adds the toggle visible action.
                button.onPress += ToggleVisible;

                // Adds the action that the button needs to handle.
                button.onPress += action.Action;

                // Names the button after the action
                button.SetName(action.Name);

                // Sets the button in use so it will be drawn later.
                button.SetInUse(true);
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
                    if (button.InUse)
                    {
                        button.gameObject.SetActive(true);
                    }
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
