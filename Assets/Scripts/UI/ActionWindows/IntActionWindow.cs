using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceHorror.UI
{
    public class IntActionWindow : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Button _applyButton, _cancelButton;
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private GameObject _window;

        private IntHandler _onApply;

        private void Awake()
        {
            _applyButton.onClick.AddListener(Apply);
            _cancelButton.onClick.AddListener(Cancel);
            _slider.onValueChanged.AddListener(OnSliderChange);
        }

        public void Init(IntHandler action, int maxValue)
        {
            _window.SetActive(true);
            _slider.maxValue = maxValue;
            _slider.value = 0;
            _onApply = action;
            OnSliderChange(0);
        }

        private void OnSliderChange(float change)
        {
            _amountText.text = change.ToString() + " / " + _slider.maxValue;
        }

        private void Apply()
        {
            _onApply?.Invoke((int)_slider.value);
            Cancel();
        }

        private void Cancel()
        {
            _onApply = null;
            _window.SetActive(false);
        }
    }
}
