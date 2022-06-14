using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.Movement
{  
    public class PlayerMouseLook : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private CharacterControllerData _controllerData;

        [SerializeField] private float _MouseSensitivity = 100f;
        [SerializeField] private float _maxVerticalRotation;

        [SerializeField] private bool _mouseSmooth;
        [SerializeField] private float _mouseStiffness = 0.5f;



        private Transform _mainBody;

        private Vector2 _lookInput;
        private PlayerInput _input;

        private float _xRotation = 0f;
        private float _yRotation = 0f;


        private void Awake()
        {
            _mainBody = gameObject.transform;
        }

        private void Start()
        {
            _input = FindObjectOfType<PlayerInput>();
            if(_input)
            {
                _input.actions["Look"].performed += OnLook;
            }
        }

        private void OnDisable()
        {
            if (_input)
            {
                _input.actions["Look"].performed -= OnLook;
            }
        }

        void FixedUpdate()
        {
            if (_controllerData.ControlsLocked) return;
            CalculateRotation();

            if (_mouseSmooth)
            {
                SmoothMouseLook();
            }
            else
            {
                RegularMouseLook();
            }
        }

        private void RegularMouseLook()
        {
            _camera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _mainBody.rotation = Quaternion.Euler(_mainBody.rotation.x, _yRotation, _mainBody.rotation.z);
        }

        private void SmoothMouseLook()
        {
            float smoothness = Mathf.Clamp01(_mouseStiffness * Time.fixedDeltaTime);
            _camera.localRotation = Quaternion.Lerp(_camera.localRotation, Quaternion.Euler(_xRotation, 0f, 0f), smoothness);
            _mainBody.rotation = Quaternion.Lerp(_mainBody.rotation, Quaternion.Euler(_mainBody.rotation.x, _yRotation, _mainBody.rotation.z), smoothness);
        }

        private void CalculateRotation()
        {
            float _mouseX = (_lookInput.x * _MouseSensitivity) * Time.fixedDeltaTime;
            float _mouseY = (_lookInput.y * _MouseSensitivity) * Time.fixedDeltaTime;

            _yRotation += _mouseX;
            _xRotation -= _mouseY;

            //Clamp Vertical look
            _xRotation = Mathf.Clamp(_xRotation, _maxVerticalRotation * -1, _maxVerticalRotation);
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            _lookInput = context.ReadValue<Vector2>();
        }
    }
}

