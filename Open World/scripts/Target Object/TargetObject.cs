using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public bool IsVisible => r.isVisible;
    public float Distance(Vector3 disTo) => transform.DistanceTo(disTo);

    [SerializeField] Renderer r;
}