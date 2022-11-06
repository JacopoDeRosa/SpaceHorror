using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoverAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _target;
    [SerializeField] private float _stoppingDistance;
    [SerializeField] private int _patrolSearchIterations = 4;
    [SerializeField] private int _patrolZoneCooldown;
    [SerializeField] DetectionAI _detector;
    [SerializeField] private List<Transform> _patrolPoints;

    WaitForSeconds distanceCheck = new WaitForSeconds(0.25f);

    private void Start()
    {
        if (_detector == null) return;
        _detector.onTargetLocked.AddListener(LockTarget);
        _detector.onTargetLost.AddListener(LooseTarget);
        StartCoroutine(Patrol());

    }

    public void OnNoiseMade(Vector3 position)
    {
        if (_target != null) return;
        print("What was noise at" + position + "?");
        StopAllCoroutines();
        StartCoroutine(SearchPoint(position, _patrolSearchIterations, ResumePatrol));
    }

    private void LockTarget(Target target)
    {
        _target = target.transform;
        StopAllCoroutines();
        StartCoroutine(FollowTarget());
    }

    private void LooseTarget(Vector3 patrolPoint)
    {
        _target = null;
        StartCoroutine(SearchPoint(patrolPoint, _patrolSearchIterations, ResumePatrol));
    }

    private void ResumePatrol()
    {
        StopAllCoroutines();
        StartCoroutine(Patrol());
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            Vector3 nextPoint = RandomPatrolPoint();
            yield return SearchPoint(nextPoint, _patrolSearchIterations, null);
        }
    }

    private IEnumerator MoveTo(Vector3 point)
    {
        if (FindMovePoint(point, out Vector3 moveTo))
        {
            _navMeshAgent.SetDestination(moveTo);
            yield return WaitForDistance(moveTo, _stoppingDistance);
        }
        else
        {
            yield return null;
        }
          
    }

    private IEnumerator WaitForDistance(Vector3 target, float distance)
    {
        while (Vector3.Distance(transform.position, target) > distance)
        {
            yield return distanceCheck;
        }
        yield return null;
    }

    private IEnumerator SearchPoint(Vector3 point, int iterations, System.Action onPointSearched)
    {
        yield return MoveTo(point);

        Debug.Log("Searching point: " + point);
        
        int currentIterations = 0;

        while (currentIterations < iterations)
        {
            Vector2 randomPlane = Random.insideUnitCircle;

            Vector3 randomPlane3D = new Vector3(randomPlane.x, 0, randomPlane.y);

            Vector3 position = point + (randomPlane3D * _patrolZoneCooldown);

            if (FindMovePoint(position, out Vector3 moveTo))
            {
                _navMeshAgent.SetDestination(moveTo);
            }

            currentIterations++;

            yield return new WaitForSeconds(_patrolZoneCooldown);
        }

        Debug.Log("Done searching point: " + point);

        onPointSearched?.Invoke();
    }

    private IEnumerator FollowTarget()
    {
        while (_target != null)
        {
            _navMeshAgent.SetDestination(_target.position);
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    private Vector3 RandomPatrolPoint()
    {
        return _patrolPoints[Random.Range(0, _patrolPoints.Count)].position;
    }

    private bool FindMovePoint(Vector3 point, out Vector3 moveTo)
    {
        bool canMove = false;

        int iterations = 1;

        NavMeshHit hit = new NavMeshHit();  

        while (canMove == false && iterations < 50)
        {
            NavMesh.SamplePosition(point, out hit, _navMeshAgent.height * 2 * iterations, NavMesh.AllAreas);

            NavMeshPath path = new NavMeshPath();

            _navMeshAgent.CalculatePath(hit.position, path);

            canMove = path.status == NavMeshPathStatus.PathComplete;

            iterations++;
        }

        moveTo = canMove ? hit.position : Vector3.zero;
        return canMove;
    }

}
