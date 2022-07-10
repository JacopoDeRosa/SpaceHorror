using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror
{
    public class CharacterAnimatorWrap : MonoBehaviour
    {
        [SerializeField] private Animator _target;

        RuntimeAnimatorController _defaultController;

        private void Awake()
        {
            _defaultController = _target.runtimeAnimatorController;
        }

        public void SetAnimatorOverride(AnimatorOverrideController controller)
        {
            if(controller == null)
            {
                Debug.LogError("Passed Override is Null");
                return;
            }

            _target.runtimeAnimatorController = controller;
        }
        public void ResetAnimatorOverride()
        {
            if (_defaultController == null)
            {
                Debug.LogError("Defualt Controller is Null");
                return;
            }

            _target.runtimeAnimatorController = _defaultController;
        }
    }
}
