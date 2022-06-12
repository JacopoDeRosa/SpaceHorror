using UnityEngine;


namespace RaycastExtensions
{
    public static class ExtendedRaycast
    {
        public static float GetSurfaceAngle(Ray ray, float distance, LayerMask layerMask)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance, layerMask))
            {
                return Vector3.Angle(hit.normal, -ray.direction);
            }
            else
            {
                return 0;
            }
        }

        public static float GetSurfaceAngle(Vector3 origin, Vector3 direction, float distance, LayerMask layerMask)
        {
            Ray ray = new Ray(origin, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance, layerMask))
            {
                return Vector3.Angle(hit.normal, -ray.direction);
            }
            else
            {
                return 0;
            }
        }
    }

}
