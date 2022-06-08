using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Reflection;

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
                button.ResetPressEvent();

                // Re-Adds the toggle visible action.
                button.onPress += ToggleVisible;

                button.SetInUse(false);
            }
        }

        private void SetSlotActions(InventorySlot slot)
        {
            BindingFlags bindingFlag = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            // Get the slot type and all its methods.
            Type slotType = slot.GetType();
            MethodInfo[] allMethods = slotType.GetMethods(bindingFlag);

            // Get the event from the buttons
            EventInfo buttonEventInfo = _buttons[0].GetType().GetEvent("onPress");
            Type eventType = buttonEventInfo.EventHandlerType;

            Type buttonAttributeType = typeof(InventoryButton);

            foreach (MethodInfo method in allMethods)
            {
                if (method.IsDefined(buttonAttributeType))
                {

                    // Get all the attributes, this helps find methods that may work with multiple types.
                    IEnumerable<InventoryButton> attributes = method.GetCustomAttributes<InventoryButton>();

                    foreach (InventoryButton attribute in attributes)
                    {
                        if (attribute.DataType == typeof(GameItemData) || slot.ItemData.GetType() == attribute.DataType)
                        {
                            SlotOptionsButton button = _freeButtons.Dequeue();
                            button.SetInUse(true);
                            button.SetName(attribute.Text);
                            Delegate handler = Delegate.CreateDelegate(eventType, slot, method);
                            buttonEventInfo.AddEventHandler(button, handler);
                        }
                    }

                }
            }

        }

        public void AddSlotAction(ButtonAction action)
        {
            if (_freeButtons.Count == 0) return;
            SlotOptionsButton button = _freeButtons.Dequeue();
            button.SetInUse(true);
            button.SetName(action.Name);
            button.onPress += action.Action;
        }

        public void SetSlot(InventorySlot slot)
        {
            ResetButtons();
            SetSlotActions(slot);
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
