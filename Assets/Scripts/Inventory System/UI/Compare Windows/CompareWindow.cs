using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


namespace SpaceHorror.InventorySystem.UI
{
    public class CompareWindow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private InventoryWindow _window;
        [SerializeField] private Image _header;

        private Vector2 _dragOffset;
        private bool _dragging;

        public event CompareWindowHandler onWindowClose;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(_header.rectTransform, eventData.position))
            {
                _dragOffset = new Vector2(transform.position.x, transform.position.y) - eventData.position;
                _dragging = true;
                transform.SetAsLastSibling();
            }      
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_dragging)
            {
                transform.position = eventData.position + _dragOffset;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dragging = false;
        }

        public void Close()
        {
            onWindowClose?.Invoke(this);
        }

        public void ResetWindow()
        {
          // TODO: Implement
        }

        public void SetInventory(Inventory inventory)
        {
            if (inventory == null) return;

            _title.text = inventory.Name;
            _window.SetInventory(inventory);
        }
    }
}
