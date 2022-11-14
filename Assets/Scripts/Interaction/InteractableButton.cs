using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FPS.Interaction;

public class InteractableButton : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;

    public UnityEvent onActivate;

    public void DeSelect()
    {
        
    }

    public string GetInteractionName()
    {
        return _prompt;
    }

    public void Interact(GameObject actor)
    {
        onActivate.Invoke();
    }

    public void Select()
    {
        
    }
}
