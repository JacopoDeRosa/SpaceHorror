using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceHorror.UI
{
    public class AudioPlayerTimer : AudioPlayerComponent
    {
        [SerializeField] private TMP_Text _currentTimeText;
        [SerializeField] private TMP_Text _maxTimeText;


        protected override void Awake()
        {
            base.Awake();
            Player.onClipSet += SetMaxTime;
        }

        private void SetMaxTime(AudioClip clip)
        {
            _maxTimeText.text = EasyExtensions.ToClockFormat(clip.length);
        }

        protected override void OnTimeUpdate(float time)
        {
            _currentTimeText.text = EasyExtensions.ToClockFormat(time);
        }
    }
}