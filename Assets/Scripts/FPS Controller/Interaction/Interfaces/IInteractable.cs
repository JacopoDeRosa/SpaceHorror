using UnityEngine;


namespace FPS.Interaction
{
    public interface IInteractable
    {
        void Interact(GameObject actor);

        void Select();

        void DeSelect();
    }
}

