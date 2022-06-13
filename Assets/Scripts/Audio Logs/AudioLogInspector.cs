using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceHorror.UI;


namespace SpaceHorror.AudioLogs
{
    public class AudioLogInspector : MonoBehaviour
    {
        [SerializeField] private AudioPlayer _audioPlayer;


        public void ResetInspector()
        {
            _audioPlayer.ResetPlayer();
        }
    }
}
