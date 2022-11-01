using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class DetectionAI : MonoBehaviour
{
    // Ai viewcone can only collide with Detection Colliders

    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _eyes;
    [SerializeField] private Target _viewConeTarget;
    [SerializeField] private Target _lockedTarget;
    [SerializeField] private Vector3 _lastKnownPosition;
    [SerializeField] private LayerMask _viewBlockMask;

    public UnityEvent<Target> onTargetLocked;
    public UnityEvent<Vector3> onTargetLost;

    private void OnTriggerEnter(Collider other)
    {
        if (_viewConeTarget == null)
        {
            _viewConeTarget = other.gameObject.GetComponent<Target>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_viewConeTarget == null) return;
        if (_viewConeTarget.gameObject == other.gameObject) _viewConeTarget = null;
    }

    private void Update()
    {
        if(_viewConeTarget != null)
        {
            if(!Physics.Linecast(_eyes.position, _viewConeTarget.CenterOfMass.position, _viewBlockMask))
            {
                LockTarget();
            }
            else
            {
                LooseTarget();
            }

            Debug.DrawLine(_eyes.position, _viewConeTarget.CenterOfMass.position);
        }
        else
        {
            LooseTarget();
        }
    }

    private void LockTarget()
    {
        if (_lockedTarget != null) return;
        _lockedTarget = _viewConeTarget;
        onTargetLocked.Invoke(_lockedTarget);
    }

    private void LooseTarget()
    {
        if (_lockedTarget == null) return;
        _lastKnownPosition = _lockedTarget.transform.position;
        _lockedTarget = null;
        onTargetLost.Invoke(_lastKnownPosition);
    }
}
