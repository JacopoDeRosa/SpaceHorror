using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform _centerOfMass;

    public Transform CenterOfMass { get => _centerOfMass; }
}
