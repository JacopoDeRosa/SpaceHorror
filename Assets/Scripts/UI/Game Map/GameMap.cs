using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.UI
{
    public class GameMap : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private int _maxLevels, _minLevels;
        [SerializeField] private int _levelHeight;

        private Vector3 _velocity;
        private int _currentLevel;

        public void OnMapOpen()
        {
            _camera.gameObject.SetActive(true);
        }
        public void OnMapClose()
        {
            _camera.gameObject.SetActive(false);
        }

        public void SetVelocity(Vector3 velocity)
        {
            velocity.y = 0;
            _velocity = velocity;
        }

        private void Update()
        {
            if(_camera.gameObject.activeInHierarchy)
            {
                _camera.Translate(_velocity * Time.deltaTime);
            }
        }
    }
}
