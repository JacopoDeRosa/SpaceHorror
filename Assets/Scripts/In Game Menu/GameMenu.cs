using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace SpaceHorror.UI
{
    public class GameMenu : MonoBehaviour
    {
      
        [SerializeField] private List<MenuWindow> _menuWindows;


        private static char[] _separators = new char[] { '/', '-', '.' };

        private MenuWindow GetMenuWindow(string name)
        {
            return _menuWindows.Find(window => window.Equals(name));
        }

        private void OpenWindow(MenuWindow window)
        {
            if (window == null) return;
            window.Open();
        }

        public void ResetMenus()
        {
            foreach (MenuWindow menu in _menuWindows)
            {
                menu.Close();
            }
        }

        public void OpenMenu(string path)
        {
            ResetMenus();

            if (string.IsNullOrEmpty(path)) return;

            string[] buffer = path.Split(_separators);

            var window = GetMenuWindow(buffer[0]);

            if (window == null) return;

            OpenWindow(window);

            if (buffer.Length <= 1) return;

            window.OpenSubmenu(buffer[1]);
        }

        public GameObject OpenMenuWithReturn(string path)
        {
            ResetMenus();

            if (string.IsNullOrEmpty(path)) return null;

            string[] buffer = path.Split(_separators);

            var window = GetMenuWindow(buffer[0]);

            if (window == null) return null;

            OpenWindow(window);

            if (buffer.Length <= 1) return window.Target;

            window.OpenSubmenu(buffer[1]);

            return window.Target;
        }
    }
}