using UnityEngine;

namespace GAMO.Hesman.Utilities
{
    public static class VectorExtension
    {
        public static Vector3 modify(this Vector3 vector3, float x = 0, float y = 0, float z = 0)
        {
            Vector3 result = vector3;
            result.x += x;
            result.y += y;
            result.z += z;
            return result;
        }
    }
}