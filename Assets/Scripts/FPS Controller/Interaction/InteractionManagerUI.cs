using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FPS.Interaction
{
    public class InteractionManagerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

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
        }

        private void OnDeSelect(IInteractable interactable)
        {
            _text.text = "";
        }
    }
}