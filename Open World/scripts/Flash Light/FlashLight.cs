using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlashLight : MonoBehaviour
{
    public Light LightObject;
    void Update()
    {
        if (NewInput.Instance.RawFire==1)
        {
            LightObject.enabled = !LightObject.enabled;
        }
    }
}