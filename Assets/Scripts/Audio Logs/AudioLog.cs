using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceHorror.Utility;

namespace SpaceHorror.AudioLogs                    
{
    [CreateAssetMenu(fileName = "New Audio Log", menuName = "Audio Log")]
    public class AudioLog : ScriptableObject
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField][TextArea] private string _transcript;
        [SerializeField] private bool _unlocked;
        [SerializeField] private string _id;
        

        public AudioClip Audio { get => _audioClip; }
        public string Transcript { get => _transcript; }
        public bool Unlocked { get => _unlocked; }
        public string Id { get => _id; }

        private void OnValidate()
        {
            if(string.IsNullOrEmpty(_id))
            {
                GenerateID();
            }
        }

        private void GenerateID()
        {
            _id = RandomID.GetBase62(RandomID.BaseIdSize);
        }

        public override bool Equals(object other)
        {
            if (other is AudioLog == false) return false;

            AudioLog log = other as AudioLog;

            return log.Id == _id;
        }
    }
}
