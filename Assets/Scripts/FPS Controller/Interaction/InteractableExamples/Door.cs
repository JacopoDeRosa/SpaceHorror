using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Interaction
{
    public class Door : MonoBehaviour, IInteractable
    {
        private bool _open;

        public void Interact(GameObject actor)
        {
            _open = !_open;
            GetComponent<Animator>().SetTrigger("Toggle");
        }
        public void Select()
        {
            
        }
        public void DeSelect()
        {

        }

        public string GetInteractionName()
        {
            if (_open)
            {
                return "Close Door";
            }
            else
            {
                return "Open Door";
            }
        }
    }
}

