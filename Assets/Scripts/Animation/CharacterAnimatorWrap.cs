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
            _target.runtimeAnimatorController = controller;
        }
        public void ResetAnimatorOverride()
        {
            _target.runtimeAnimatorController = _defaultController;
        }
    }
}
