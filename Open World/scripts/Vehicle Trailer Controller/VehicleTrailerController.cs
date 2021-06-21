using System.Collections.Generic;
using UnityEngine;

public class VehicleTrailerController : MonoBehaviour
{
    public List<VehicleWheelLayout> TrailerWheels;

    Transform VehicleTransform;
    Rigidbody VehicleBody;

    public void Start()
    {
        VehicleTransform = transform;
        VehicleBody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        foreach (VehicleWheelLayout wheel in TrailerWheels)
        {
            wheel.UpdateWheel(0, 0, 0,false);
            wheel.UpdateSuspention(VehicleTransform, VehicleBody);
        }
    }
}
