using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FPS.Movement
{
    public class PlayerCameraPosition : MonoBehaviour
    { 
        [SerializeField] Transform _camera;
        [SerializeField] private CharacterControllerData _controllerData;
        [SerializeField] private PlayerStanceHandler _stanceHandler;
      

        [SerializeField] private bool _headbob;
        [SerializeField] private AnimationCurve _headbobProfileX;
        [SerializeField] private AnimationCurve _headbobProfileY;

        [SerializeField] private float _headbobSpeed = 1;
        [SerializeField] private Vector2 _headbobFactor = new Vector2(2, 1);
        [SerializeField] private float _sprintSpeedMultiplier = 2;
        [SerializeField] private float _crouchSpeedMultiplier = 0.8f;

        private float _speedActual;
        private float _evaluationActual;

        [SerializeField] private float _stanceChangeTime = 0.1f;
        [SerializeField] Vector3 _normalPosition;
        [SerializeField] Vector3 _crouchedPosition;

        private float _cameraPosition = 0f;

        private void Awake()
        {
            _stanceHandler.onStanceChange += SetHeadbobSpeed;
            _stanceHandler.onStanceChange += MoveCamera;
        }

        void FixedUpdate()
        {
            if (_controllerData.ControlsLocked) return;
            // Update Headbob
            _camera.localPosition = Vector3.Lerp(_normalPosition, _crouchedPosition, _cameraPosition) + HeadBobVector() * CurrentSpeed();      
        }

        private Vector3 HeadBobVector()
        {
            if (_headbob == false || _controllerData.IsGrounded == false) return Vector3.zero;
            _evaluationActual += _speedActual * Time.fixedDeltaTime;

            float evaluationX = _headbobProfileX.Evaluate(_evaluationActual) * _headbobFactor.x;
            float evaluationY = _headbobProfileY.Evaluate(_evaluationActual) * _headbobFactor.y;
           

            return new Vector3(evaluationX, evaluationY, 0);
        }

        private void SetHeadbobSpeed(Stance stance)
        {
            if(stance == Stance.Running)
            {
                _speedActual = _headbobSpeed * _sprintSpeedMultiplier;
            }
            else if(stance == Stance.Prone)
            {
                _speedActual = _headbobSpeed * _crouchSpeedMultiplier;
            }
            else
            {
                _speedActual = _headbobSpeed;
            }
        }
        private void MoveCamera(Stance stance)
        {
            if(stance == Stance.Prone)
            {
                StartCoroutine(LowerCamera(_stanceChangeTime));
            }
            else if(stance == Stance.Standing || stance == Stance.Running)
            {
                StartCoroutine(RaiseCamera(_stanceChangeTime));
            }

        }
        private float CurrentSpeed()
        {
            if (_controllerData.IsGrounded == false) return 0;
            return Mathf.Clamp(Mathf.Abs(_controllerData.PlanarInput.x) + Mathf.Abs(_controllerData.PlanarInput.z), 0, 1);
        }

        private IEnumerator LowerCamera(float time)
        {
            while (_cameraPosition < 1)
            {
                _cameraPosition += (1 / time) * Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }
        }
        private IEnumerator RaiseCamera(float time)
        {
            while(_cameraPosition > 0)
            {
                _cameraPosition -= (1 / time) * Time.fixedDeltaTime;

                yield return new WaitForFixedUpdate();
            }
        }


       
    }
}

