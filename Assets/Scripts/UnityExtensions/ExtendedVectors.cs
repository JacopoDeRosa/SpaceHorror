using UnityEngine;

namespace VectorExtensions
{
    public static class ExtendedVectors
    {
        public static Vector3 DistanceVector(this Vector3 vector, Vector3 target)
        {
            if (target == null)
            {
                Debug.LogError("Warning target Vector can't be null");
                return Vector3.zero;
            }
            return target - vector;
        }
    }
}

