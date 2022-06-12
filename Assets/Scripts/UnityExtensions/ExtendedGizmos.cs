using UnityEngine;


namespace GizmosExtensions
{
    public static class ExtendedGizmos
    {
        /// <summary>
        /// Draws two spheres connected by lines to simulate a wire capsule
        /// </summary>
        /// <param name="start"></param>
        /// <param name="radius"></param>
        /// <param name="height"></param>
        public static void DrawWireCapsule(Vector3 start, float radius, float height)
        {
            Gizmos.DrawWireSphere(start + new Vector3(0, radius, 0), radius);
            Gizmos.DrawWireSphere(start + new Vector3(0, height - radius, 0), radius);
            Gizmos.DrawLine(start + new Vector3(radius, radius, 0), start + new Vector3(radius, height - radius, 0));
            Gizmos.DrawLine(start + new Vector3(-radius, radius, 0), start + new Vector3(-radius, height - radius, 0));
            Gizmos.DrawLine(start + new Vector3(0, radius, radius), start + new Vector3(0, height - radius, radius));
            Gizmos.DrawLine(start + new Vector3(0, radius, -radius), start + new Vector3(0, height - radius, -radius));
        }

        /// <summary>
        /// End and Start vectors must be vertically aligned
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="radius"></param>
        public static void DrawWireCapsule(Vector3 start, Vector3 end,float radius)
        {
            if(start.x != end.x || start.z != end.z)
            {
                Debug.LogError("Warning DrawWireCapsule method start and end points can't be misalligned");
                return;
            }

            float height = Vector3.Distance(end, start);
            Gizmos.DrawWireSphere(start + new Vector3(0, radius, 0), radius);
            Gizmos.DrawWireSphere(start + new Vector3(0, height - radius, 0), radius);
            Gizmos.DrawLine(start + new Vector3(radius, radius, 0), start + new Vector3(radius, height - radius, 0));
            Gizmos.DrawLine(start + new Vector3(-radius, radius, 0), start + new Vector3(-radius, height - radius, 0));
            Gizmos.DrawLine(start + new Vector3(0, radius, radius), start + new Vector3(0, height - radius, radius));
            Gizmos.DrawLine(start + new Vector3(0, radius, -radius), start + new Vector3(0, height - radius, -radius));
        }
    }
}

