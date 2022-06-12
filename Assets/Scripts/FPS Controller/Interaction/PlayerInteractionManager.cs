using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FPS.Interaction
{
    public class PlayerInteractionManager : MonoBehaviour
    {
        [SerializeField] private float _interactionRange;
        [SerializeField] private Transform _camera;

        private IInteractable _target;
   
        void Update()
        {
         SetTarget();
         Interact();
        }

        private void SetTarget()
        {
         RaycastHit hitInfo;
         Ray ray = new Ray(_camera.position + _camera.forward / 4, _camera.forward);

         if (Physics.Raycast(ray, out hitInfo, _interactionRange) && hitInfo.transform.GetComponent<IInteractable>() != null)
         {                   
          _target = hitInfo.transform.GetComponent<IInteractable>();
          _target.Select();          
         }
         else
         {
          if (_target != null)
          {
           _target.DeSelect();
           _target = null;
          }       
         }
        }

        private void Interact()
        {
          if (_target == null) return;

          if(Input.GetKeyDown(KeyCode.E))
            { 
              _target.Interact(_camera.gameObject);
              _target = null; // Setting target to null prevents conflicts if target is destroyed in use
            }

           
        }
    }
}

