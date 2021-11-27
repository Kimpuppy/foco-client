using UnityEngine;

namespace Jaeyun
{
    public static class MathUtil
    {
        public static float AngleTo360(float angle)
        {
            return angle < 0 ? angle % 360 + 360 : angle % 360f;
        }
        
        public static float LookAt2D(Vector3 from, Vector3 to)
        {
            float angle = Mathf.Atan2(to.y - from.y, to.x - from.x) * Mathf.Rad2Deg;
            return AngleTo360(angle);
        }
    }
}