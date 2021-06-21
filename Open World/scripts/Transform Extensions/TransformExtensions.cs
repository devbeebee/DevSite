using UnityEngine;

static class TransformExtensions
{
    public static float DistanceTo(this Transform tr, Vector3 toPos) => Vector3.Distance(tr.position, toPos);
}