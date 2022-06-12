using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace SpaceHorror.UI
{
    [Serializable]
    public class Menu
    {

        [SerializeField] protected string _name = "Unnamed Menu";

        [SerializeField] protected GameObject _target;


        public UnityEvent onMenuOpen;


        public UnityEvent onMenuClose;

        public bool Equals(string name)
        {
            if (name == _name) return true;
            return false;
        }

        public void Open()
        {
            OnOpen();
            if (_target != null) _target.SetActive(true);
            onMenuOpen.Invoke();
        }
        public void Close()
        {
            onMenuClose.Invoke();
            if (_target != null) _target.SetActive(false);
        }
        protected virtual void OnOpen()
        {

        }
        protected virtual void OnClose()
        {
            onMenuClose.Invoke();
        }
    }
}