using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceHorror.UI;

namespace SpaceHorror.InventorySystem.UI
{
    public class CompareWindowsManager : MonoBehaviour
    {
        [SerializeField] private InGameMenuManager _inGameMenu;
        [SerializeField] private CompareWindow[] _allWindows;

        private Queue<CompareWindow> _freeWindows;


        private void Start()
        {
            ResetWindows();
        }

        public void ResetWindows()
        {
            foreach (CompareWindow window in _allWindows)
            {
                window.ResetWindow();
                window.gameObject.SetActive(false);
            }
            _freeWindows = new Queue<CompareWindow>(_allWindows);
        }


        public void DisplayInventoryWindow(Inventory inventory)
        {
            if (_freeWindows.Count == 0) return;

            if(_inGameMenu.MenuOpen == false)
            {
                _inGameMenu.OpenMenu("Inventory");
            }

            var window = _freeWindows.Dequeue();
            window.gameObject.SetActive(true);
            window.SetInventory(inventory);
        }

    }
}
