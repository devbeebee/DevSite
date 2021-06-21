using UnityEngine;

public abstract class CameraRaycaster : MonoBehaviour
{
    protected Camera cam;
    protected RaycastHit currentHit;
    [SerializeField] [Range(0, 150)] protected float rayRange;
    public float RangeFromObject;
    public bool ShootRay()
    {
        if (Physics.Raycast(cam.CenterScreen(), cam.transform.forward, out currentHit, rayRange))
        {
            RangeFromObject = transform.DistanceTo(currentHit.point);
            return true;
        }
        return false;
    }
}
