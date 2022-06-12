using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Interaction
{
    public class Door : MonoBehaviour, IInteractable
    {
        public void Interact(GameObject actor)
        {
            GetComponent<Animator>().SetTrigger("Toggle");
        }
        public void Select()
        {
            
        }
        public void DeSelect()
        {

        }
    }
}

