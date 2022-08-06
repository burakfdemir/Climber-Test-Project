using UnityEngine;

namespace Utilities
{
    public static class VectorExtensions
    {
        public static bool IsApproximately(this Vector3 vector, Vector3 compareVector)
        {
            return Mathf.Approximately(vector.x, compareVector.x) &&
                   Mathf.Approximately(vector.y, compareVector.y) &&
                   Mathf.Approximately(vector.z, compareVector.z);
        }
    }
}
