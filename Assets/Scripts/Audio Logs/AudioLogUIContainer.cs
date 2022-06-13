using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace SpaceHorror.AudioLogs
{
    public class AudioLogUIContainer : MonoBehaviour, IPointerDownHandler
    {
        private const string lockedName = "????";

        [SerializeField] private TMP_Text _text;

        private AudioLogWindow _parentWindow;
        private AudioLogInspector _inspector;
        private AudioLog _log;

        public void SetParentWindow(AudioLogWindow window)
        {
            _parentWindow = window;
        }

        public void SetInspector(AudioLogInspector inspector)
        {
            _inspector = inspector;
        }

        public void SetAudioLog(AudioLog log)
        {
            _log = log;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            print("Inspect " + _log.name);
        }
    }
}
