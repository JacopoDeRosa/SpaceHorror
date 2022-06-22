using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Interaction
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private AudioClip _openAudio, _closeAudio;
        [SerializeField] private string _name = "Door";
        [SerializeField] private Animator _animator;
        private bool _open;

        public void Interact(GameObject actor)
        {
            _open = !_open;
            _animator.SetBool("Open", _open);
        }
        public void Select()
        {
            
        }
        public void DeSelect()
        {

        }

        public string GetInteractionName()
        {
            if (_open)
            {
                return "Close " + _name;
            }
            else
            {
                return "Open " + _name;
            }
        }

        public void OnOpen()
        {
            if (_openAudio == null) return;
            AudioSource.PlayClipAtPoint(_openAudio, transform.position);
        }

        public void OnClose()
        {
            if (_closeAudio == null) return;
            AudioSource.PlayClipAtPoint(_closeAudio, transform.position);
        }
    }
}

