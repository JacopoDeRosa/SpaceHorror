using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPS.Interaction;

namespace SpaceHorror.InventorySystem
{
    public class InventoryInteractionWrapper : MonoBehaviour, IInteractable
    {
        [SerializeField] private Inventory _target;

        public void DeSelect()
        {

        }

        public void Interact(GameObject actor)
        {
            print("Opening " + _target.Name);

        }

        public void Select()
        {

        }

        public string GetInteractionType()
        {
            return "Open";
        }
    }
}
