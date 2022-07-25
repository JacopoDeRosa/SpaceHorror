using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FPS.Interaction
{
    public class Draggable : MonoBehaviour
    {
        [SerializeField] private LayerMask _pickUpMask;

        private GameObject _actor;
        private Rigidbody _rigidbody;
        private LayerMask _startingMask;
        private bool _isDragged;
        private float _actorForce;
        private float _holdingDistance;

        private void Awake()
        {
            _startingMask = gameObject.layer;
            _rigidbody = GetComponent<Rigidbody>();
        }


        private void FixedUpdate()
        {
            if(_isDragged)
            {
                Vector3 targetPosition = _actor.transform.position + _actor.transform.forward * _holdingDistance;
                _rigidbody.AddForce((targetPosition - transform.position) * _actorForce);
            }
        }

        public void Drag(GameObject actor, float actorForce, float holdingDistance)
        {
            _isDragged = !_isDragged;

            _rigidbody.useGravity = !_rigidbody.useGravity;

            // switch Actor
            if(_actor != null)
            {
                _actor = null;          
            }
            else
            {
                _actor = actor;
                _actorForce = actorForce;
                _holdingDistance = holdingDistance;
            }

            // switch Mask
            if(gameObject.layer == _startingMask)
            {
                gameObject.layer = _pickUpMask.value;
            }
            else
            {
                gameObject.layer = _startingMask.value;
            }
        }

        public void Select()
        {
         
        }

        public void DeSelect()
        {
           
        }
    }
}

