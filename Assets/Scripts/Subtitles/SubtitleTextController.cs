using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace SpaceHorror.UI
{
    public class SubtitleTextController : UIElement
    {
        [SerializeField] private TMP_Text _mainText;
        [SerializeField] private TMP_Text _nameText;

        public event Action onDisplay;
        public event Action onDisplayEnd;

        private bool _busy;

        private void Start()
        {
            _mainText.gameObject.SetActive(false);
        }


        public void DisplaySubtitle(Color senderColor, string senderName, string content, float duration)
        {
            if (_busy) return;
            StartCoroutine(DrawSubtitle(duration, senderColor, senderName, content));
        }

        public void DisplaySubtitle(Subtitle subtitle)
        {
            DisplaySubtitle(subtitle.SenderColor, subtitle.SenderName, subtitle.Content, subtitle.Duration);
        }

        private IEnumerator DrawSubtitle(float time, Color senderColor, string senderName, string content)
        {
            _busy = true;
            onDisplay?.Invoke();
            _mainText.gameObject.SetActive(true);

            _nameText.color = senderColor;
            _nameText.text = senderName;
            _mainText.text = content;

            yield return new WaitForSeconds(time);

            _mainText.gameObject.SetActive(false);

            onDisplayEnd?.Invoke();

            _busy = false;
        }
    }
}