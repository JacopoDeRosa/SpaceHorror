using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceHorror.UI
{
    public class IntActionWindow : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Button _applyButton, _cancelButton;
        [SerializeField] private GameObject _window;

        private IntHandler _onApply;

        public void Init(IntHandler action, int maxValue)
        {
            _window.SetActive(true);
            _slider.maxValue = maxValue;
            _slider.value = 0;
            _onApply = action;
        }

        private void Apply()
        {
            _onApply.Invoke((int)_slider.value);
            Cancel();
        }

        private void Cancel()
        {
            _onApply = null;
            _window.SetActive(false);
        }
    }
}
