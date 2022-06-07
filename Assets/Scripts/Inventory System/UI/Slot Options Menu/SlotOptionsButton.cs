using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.EventSystems;

namespace SpaceHorror.InventorySystem.UI
{
    public class SlotOptionsButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private TMP_Text _text;

        private bool _inUse;

        public event Action onPress;

        public bool InUse { get => _inUse; }

        public void SetName(string name)
        {
            _text.text = name;
        }

        public void SetInUse(bool inUse)
        {
            _inUse = inUse;
        }

        public void ResetPressEvent()
        {
            onPress = null;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                onPress?.Invoke();
                print("Pressed");
            }
        }
    }
}