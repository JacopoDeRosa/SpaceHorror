using UnityEngine;


namespace FPS.Interaction
{
    public interface IInteractable
    {
        public void Interact(GameObject actor);

        public void Select();

        public void DeSelect();

        public string GetInteractionName();
    }
}

