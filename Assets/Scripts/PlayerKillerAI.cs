using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using FPS.Movement;
using UnityEngine.AI;

public class PlayerKillerAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private LayerMask _checkMask;

    private bool _playerFound = false;

    public UnityEvent onPlayerHit;

    private Vector3 _extents;

    private void Awake()
    {
        _extents = new Vector3(0.5f, 1, 0.5f);
    }

    private void FixedUpdate()
    {
        if(_playerFound == false && Physics.CheckBox(transform.position + Vector3.up, _extents, transform.rotation, _checkMask))
        {
            _playerFound = true;
            _camera.gameObject.SetActive(true);
            _navMeshAgent.enabled = false;
            Destroy(FindObjectOfType<CharacterControllerWrapper>().gameObject);
            onPlayerHit?.Invoke();
        }
    }
}
