using UnityEngine;

static class CameraExtensions
{
    static Vector3 ScreenCenter = new Vector3(0.5f, 0.5f, 0);
    public static Vector3 CenterScreen(this Camera c) => c.ViewportToWorldPoint(ScreenCenter);
}