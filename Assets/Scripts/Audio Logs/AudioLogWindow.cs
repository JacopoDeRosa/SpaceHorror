using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceHorror.AudioLogs
{
    public class AudioLogWindow : MonoBehaviour
    {
        [SerializeField] private AudioLogInspector _inspector;
        [SerializeField] private AudioLogUIContainer[] _allContainers;

        private Queue<AudioLogUIContainer> _freeContainers;

        public static AudioLog[] LoadAllAudioLogs()
        {
           return Resources.LoadAll<AudioLog>("Audio Logs");
        }

        private void Start()
        {
            ResetContainers();

            foreach (AudioLog log in LoadAllAudioLogs())
            {
                var container = _freeContainers.Dequeue();
                container.gameObject.SetActive(true);
                container.SetAudioLog(log);
            }
        }

        private void ResetContainers()
        {
            foreach (AudioLogUIContainer container in _allContainers)
            {
                container.SetParentWindow(this);
                container.SetInspector(_inspector);
                container.gameObject.SetActive(false);
            }

            _freeContainers = new Queue<AudioLogUIContainer>(_allContainers);
        }
    }
}
