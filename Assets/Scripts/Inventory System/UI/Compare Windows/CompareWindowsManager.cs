using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.InventorySystem.UI
{
    public class CompareWindowsManager : MonoBehaviour
    {
        [SerializeField] private CompareWindow[] _allWindows;

        private Queue<CompareWindow> _freeWindows;


        private void Start()
        {
            ResetWindows();
        }

        private void ResetWindows()
        {
            foreach (CompareWindow window in _freeWindows)
            {
                window.ResetWindow();
                window.gameObject.SetActive(false);
            }
            _freeWindows = new Queue<CompareWindow>(_allWindows);
        }

        public void DisplayInventory(Inventory inventory)
        {
            if (_freeWindows.Count == 0) return;

            var window = _freeWindows.Dequeue();

            window.gameObject.SetActive(true);

            window.SetInventory(inventory);
        }

    }
}
