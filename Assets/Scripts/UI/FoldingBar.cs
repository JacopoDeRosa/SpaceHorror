using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoldingBar : MonoBehaviour
{
    [SerializeField] private Directions _direction;
    [SerializeField] private float _sinkAmount;
    [SerializeField] private float _sinkSpeed = 1;
    [SerializeField] private CanvasScaler _scaler;
    [SerializeField] private bool _startStatus;
    [SerializeField] private bool _open;


    private bool _busy;
    private bool _wantedStatus;  

    private float _adjustedSinkAmount;


    public bool Open { get => _open; }


    private void Awake()
    {
       FindSinkAmount();
    }
    private void Start()
    {
        
        Toggle(_startStatus);
    }

    private float FindScale()
    {
        return Camera.main.pixelWidth / _scaler.referenceResolution.x;
    }

    private void FindSinkAmount()
    {
        _adjustedSinkAmount = _sinkAmount * FindScale();
    }

    public void Toggle(bool open)
    {
        _wantedStatus = open;
        if (_busy) return;
        if (open == true && _open == false)
        {
            _open = true;
            StartCoroutine(MoveToPosition(0));

        }
        else if (open == false && _open)
        {
            _open = false;
            StartCoroutine(MoveToPosition(_adjustedSinkAmount));
        }
    }


    public IEnumerator Toggle()
    {
        if (_busy) yield break;

        if (_open == false)
        {
            _open = true;
            yield return MoveToPosition(0);       
        }
        else if (_open)
        {
            _open = false;
            yield return MoveToPosition(_adjustedSinkAmount);
        }

        if(_open != _wantedStatus)
        {
            Debug.Log("should toggle");
            Toggle(_wantedStatus);
        }
    }
    private IEnumerator MoveToPosition(float position)
    {
        _busy = true;
        float t = 0;
        Vector3 start = transform.position;
        Vector3 end = Vector3.zero;

        int open = -1;
        if (_open == false)
        {
            open = 1;
        }

        switch (_direction)
        {
            case Directions.Top:
                end = new Vector3(start.x, start.y + _adjustedSinkAmount * open, start.z);
                break;

            case Directions.Right:
                end = new Vector3(start.x + _adjustedSinkAmount * open, start.y, start.z);
                break;

            case Directions.Bottom:
                end = new Vector3(start.x, start.y - _adjustedSinkAmount * open, start.z);
                break;

            case Directions.Left:
                end = new Vector3(start.x - _adjustedSinkAmount * open, start.y, start.z);
                break;
        }


        // Declaring wait once reduces garbage
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        while (t < 1)
        {
            transform.position = Vector3.Lerp(start, end, t);
            t += Time.fixedDeltaTime * _sinkSpeed;
            yield return wait;
        }

        _busy = false;

        if (_open != _wantedStatus)
        {
            Toggle(_wantedStatus);
        }
    }
}