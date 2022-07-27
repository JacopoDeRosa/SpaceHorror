using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace SpaceHorror.UI
{
    public class MapArrowButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameMap _map;
        [SerializeField] private Vector3 _velocity;

        public void OnPointerDown(PointerEventData eventData)
        {
            _map.SetVelocity(_velocity);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _map.SetVelocity(Vector3.zero);
        }
    }
}
