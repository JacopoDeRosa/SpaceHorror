using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Interaction
{
    public class UsableTest : MonoBehaviour, IInteractable
    {
       public void Interact(GameObject actor)
       {
        print("Interacted");
       }

       public void Select()
       {      
        GetComponent<MeshRenderer>().material.color = Color.red;
       }

       public void DeSelect()
       {
        GetComponent<MeshRenderer>().material.color = Color.white;
       }

        public string GetInteractionType()
        {
            return "Interact";
        }
    }
}
    


