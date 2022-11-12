using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPS.Interaction;

namespace SpaceHorror.InventorySystem.UI
{
    public class InventoryInteractionWrapper : MonoBehaviour, IInteractable
    {
        [SerializeField] private Inventory _targetInventory;
        [SerializeField] private string _prompt = "Open";

        public void DeSelect()
        {

        }

        public void Interact(GameObject actor)
        {
          var manager = FindObjectOfType<CompareWindowsManager>();
            if(manager)
            {
                manager.DisplayInventoryWindow(_targetInventory);
            }
        }

        public void Select()
        {

        }

        public string GetInteractionName()
        {
            return _prompt + " " + _targetInventory.Name;
        }
    }
}
