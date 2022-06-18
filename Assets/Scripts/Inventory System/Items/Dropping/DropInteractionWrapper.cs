using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPS.Interaction;

namespace SpaceHorror.InventorySystem
{
    public class DropInteractionWrapper : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameItemDrop _drop;

        public void DeSelect()
        {
            
        }
        public void Select()
        {
            
        }

        public string GetInteractionName()
        {
            return "Pick Up " + _drop.ItemData.name + " (" + _drop.ItemCount.ToString() + ")";
        }

        public void Interact(GameObject actor)
        {
            Inventory inventory = actor.GetComponent<Inventory>();

            if (inventory == null)
            {
                Debug.Log("No Inventory On Actor");
                return;
            }
                

            ItemSlot slot = new ItemSlot(_drop.ItemData, inventory, _drop.ItemCount, _drop.ItemParameters); 

            if (inventory.TryPlaceItem(slot))
            {
                Destroy(gameObject);
            }      
        }


    }
}
