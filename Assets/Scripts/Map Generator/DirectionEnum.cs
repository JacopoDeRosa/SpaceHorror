using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SpaceHorror
{
    [Flags]
    public enum Direction
    {
        Left = 1,
        Forward = 2,
        Right = 4,
        Back = 8
    }
}

