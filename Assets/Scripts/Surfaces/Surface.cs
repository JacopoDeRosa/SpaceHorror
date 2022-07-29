using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    [SerializeField] private SurfaceTypes _surfaceType;

    public SurfaceTypes SurfaceType { get => _surfaceType; }
}
