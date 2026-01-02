using UnityEngine;

public static class Utils
{
    public static float GetAngle(Vector2 position, Vector2 origin)
    {
        Vector2 dir = (position - origin).normalized;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }


    public static Vector3 ProjectIn3D(Vector2 dir2d, float verticalValue = 0)
    {
        Vector3 dir3d = Vector3.zero;

        dir3d.x = dir2d.x;
        dir3d.y = verticalValue;
        dir3d.z = dir2d.y;

        return dir3d;
    }

    public static Vector2 AdjustToCamera(Vector2 dir)
    {
        Transform cam = Camera.main.transform;

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 ret = forward * dir.y + right * dir.x;

        return new Vector2(ret.x, ret.z);
    }

}
