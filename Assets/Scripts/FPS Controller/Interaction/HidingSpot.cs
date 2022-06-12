using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Interaction
{
    public class HidingSpot : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _virtualCamera;

        private bool _active;
        private GameObject _actor;


        public void DeSelect()
        {
  
        }

        public void Interact(GameObject actor)
        {
            _virtualCamera.SetActive(true);
            _actor = actor.transform.root.gameObject;
            _actor.SetActive(false);
            _active = true;
        }

        public void Select()
        {
            
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E) && _active)
            {
                _virtualCamera.SetActive(false);
                _actor.SetActive(true);
                _active = false;
                _actor = null;
            }
        }
    }
}
