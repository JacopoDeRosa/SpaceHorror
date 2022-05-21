using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRoom : MonoBehaviour
{
    public const string OccluderTag = "Player";

    [SerializeField] private MapRoom[] _connectedRooms;
    [SerializeField] private Direction _connections;
    [SerializeField] private Transform _entrance;
    [SerializeField] private MeshRenderer[] _childRenderers;

    private bool _inUse;
    private bool _visible;

    private void OnValidate()
    {
        _childRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void SetVisible(bool visible)
    {
        if (_inUse && _visible) return;

        foreach (MeshRenderer renderer in _childRenderers)
        {
            if(visible)
            {
                renderer.enabled = true;
            }
            else
            {
                renderer.enabled = false;
            }
        }

        _visible = visible;
    }

    public void SetConnectedVisible(bool visible)
    {
        foreach (var room in _connectedRooms)
        {
            room.SetVisible(visible);
        }
    }

    public void SetConnectedRooms(MapRoom[] rooms)
    {
        _connectedRooms = rooms;
    }

    public void Init()
    {
        SetVisible(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == OccluderTag)
        {
            SetVisible(true);
            SetConnectedVisible(true);
            _inUse = true;         
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == OccluderTag)
        {
            SetVisible(false);
            SetConnectedVisible(false);
            _inUse = false;
        }
    }
}
