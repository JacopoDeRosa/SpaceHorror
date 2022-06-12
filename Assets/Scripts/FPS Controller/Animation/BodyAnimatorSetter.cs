using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Movement
{
    public class BodyAnimatorSetter : MonoBehaviour
    {
        [SerializeField] private CharacterControllerWrapper _controllerWrapper;
        [SerializeField] private PlayerStanceHandler _stanceHandler;
        [SerializeField] private Animator _animator;



        private void Awake()
        {
            _stanceHandler.onStanceChange += OnStanceChange;
        }

        private void OnStanceChange(Stance stance)
        {
            _animator.ResetTrigger("Crouch");
            _animator.ResetTrigger("Stand");
            if (stance == Stance.Prone)
            {
                _animator.SetTrigger("Crouch");
            }
            else
            {
                _animator.SetTrigger("Stand");
            }
        }

        private void Update()
        {
            _animator.SetFloat("Speed X", _controllerWrapper.Velocity.x);
            _animator.SetFloat("Speed Z", _controllerWrapper.Velocity.z);
        }



    }
}
