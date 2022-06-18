using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SpaceHorror.UI;

namespace FPS.Interaction
{
    public class InteractionUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Crosshair _crosshair;
        [SerializeField] private float _crosshairOnSelect, _crosshairDefault;

        private PlayerInteraction _manager;

        private void Awake()
        {
            _manager = FindObjectOfType<PlayerInteraction>();
            if(_manager)
            {
                _manager.onSelected += OnSelect;
                _manager.onDeSelected += OnDeSelect;
            }
        }


        private void OnDisable()
        {
            if (_manager)
            {
                _manager.onSelected -= OnSelect;
                _manager.onDeSelected -= OnDeSelect;
            }
        }

        private void OnSelect(IInteractable interactable)
        {
            _text.text = interactable.GetInteractionName();
            _crosshair.SetSizeSmooth(_crosshairOnSelect);
        }

        private void OnDeSelect(IInteractable interactable)
        {
            _crosshair.SetSizeSmooth(_crosshairDefault);
            _text.text = "";
        }
    }
}