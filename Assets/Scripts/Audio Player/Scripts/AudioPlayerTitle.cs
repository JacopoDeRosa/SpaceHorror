using UnityEngine;
using UnityEngine.UI;

namespace SpaceHorror.UI
{
    public class AudioPlayerTitle : AudioPlayerComponent
    {
        [SerializeField] private Text _text;

        protected override void Awake()
        {
            base.Awake();
            Player.onClipSet += SetTitle;
        }

        private void SetTitle(AudioClip audioClip)
        {
            if (audioClip == null) return;
            _text.text = audioClip.name;
        }
    }
}