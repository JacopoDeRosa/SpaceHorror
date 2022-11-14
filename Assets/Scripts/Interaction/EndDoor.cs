using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FPS.Interaction;
using SpaceHorror.InventorySystem;

public class EndDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private GameItemData _keyCardData;
    [SerializeField] private string _prompt;

    public void DeSelect()
    {
        
    }

    public string GetInteractionName()
    {
        return _prompt;
    }

    public void Interact(GameObject actor)
    {
        if(actor.TryGetComponent<Inventory>(out Inventory inv) && inv.ContainsItem(_keyCardData))
        {
            Debug.Log("Player Wins");
        }
    }

    public void Select()
    {
        
    }
}
