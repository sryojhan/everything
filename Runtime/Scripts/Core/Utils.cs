using log4net.Util;
using UnityEngine;

public static class Utils
{
    public static float GetAngle(Vector2 position, Vector2 origin)
    {
        Vector2 dir = (position - origin).normalized;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}
