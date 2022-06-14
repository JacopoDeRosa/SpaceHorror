using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceHorror.UI
{
    public class GameMenuInput : MonoBehaviour
    {
        [SerializeField] GameMenu _menu;

        private PlayerInput _input;

        private bool _menuOpen;

        void Start()
        {
            _input = FindObjectOfType<PlayerInput>();

            if (_input)
            {
                _input.actions["Inventory"].started += OnInventory;
                _input.actions["Map"].started += OnMap;
            }
        }

        private void OnDisable()
        {
            if (_input)
            {
                _input.actions["Inventory"].started -= OnInventory;
                _input.actions["Map"].started -= OnMap;
            }
        }


        private void OnInventory(InputAction.CallbackContext context)
        {
            OpenMenu("Inventory");
        }

        private void OnMap(InputAction.CallbackContext context)
        {
            OpenMenu("Map");
        }


        private void OpenMenu(string menu)
        {
            if (_menu.gameObject.activeInHierarchy) return;
            _menu.gameObject.SetActive(true);
            _menu.OpenMenu(menu);
            GameStatus.SetMenuFocus(true);
        }

        public void CloseMenu()
        {
            _menu.ResetMenus();
            _menu.gameObject.SetActive(false);
            GameStatus.SetMenuFocus(false);
        }
    }
}