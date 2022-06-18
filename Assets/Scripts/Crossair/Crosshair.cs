using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceHorror.UI
{
    public class Crosshair : UIElement
    {
        [System.Serializable]
        private class CrosshairBit
        {     
            [SerializeField] private Vector3 _startPos;
            [SerializeField] private Vector3 _offset;
            [SerializeField] private Transform _target;

            public Transform transform { get => _target; }
            public Vector3 Offset { get => _offset; }
            public Vector3 StartPos { get => _startPos; }
        }

        [SerializeField] [Range(0, 1)] private float _size;
        [SerializeField] private Color _crossairColor = Color.white;
        [SerializeField] private List<CrosshairBit> _crossairBits = new List<CrosshairBit>();


        private void OnValidate()
        {
            ChangeSize();
            ChangeColors();
        }
     
        private void ChangeSize()
        {
            foreach (var bit in _crossairBits)
            {
                if (bit == null || bit.transform == null) continue;
                bit.transform.localPosition = Vector3.Lerp(bit.StartPos, bit.StartPos + bit.Offset, _size);
            }
        }
        private void ChangeColors()
        {
            foreach (Image image in GetComponentsInChildren<Image>())
            {
                image.color = _crossairColor;
            }
        }

        public void SetSize(float size)
        {
            size = Mathf.Clamp(size, 0, 1);
            _size = size;
            ChangeSize();
        }
        public void SetColor(Color color)
        {
            _crossairColor = color;
        }

        public void SetSizeSmooth(float size)
        {
            size = Mathf.Clamp(size, 0, 1);
            StopAllCoroutines();
            StartCoroutine(SmootSizeChange(size));
        }

        private IEnumerator SmootSizeChange(float size)
        {
            WaitForFixedUpdate wait = new WaitForFixedUpdate();
            if(_size > size)
            {
                while(_size > size)
                {
                    _size -= Time.fixedDeltaTime;
                    ChangeSize();
                    yield return wait;
                }
            }
            else if(_size < size)
            {
                while (_size < size)
                {
                    _size += Time.fixedDeltaTime;
                    ChangeSize();
                    yield return wait;
                }
            }
        }
    }
}



