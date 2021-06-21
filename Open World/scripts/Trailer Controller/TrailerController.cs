using System.Collections.Generic;
using UnityEngine;


public class TrailerController : MonoBehaviour
{
    [SerializeField] BaseVehicleTrailerScriptable trailerScriptable;
    [SerializeField] List<VehicleWheelLayout> VehicleWheels;
    [SerializeField] bool TrailerBrake = false;

    public void Start()
    {
        if (trailerScriptable.wheelShape != null)
        {
            for (int i = 0; i < VehicleWheels.Count; i++)
            {
                var wheel = VehicleWheels[i];
                var ws = GameObject.Instantiate(trailerScriptable.wheelShape,Vector3.zero,Quaternion.identity, wheel.transform);
                wheel.WheelShape = ws.transform;
                wheel.UpdateWheel(0f, .1f, 0f,false);
            }
        }
    }
    private void Update()
    {
        if (TrailerBrake)
        {
            foreach (VehicleWheelLayout wheel in VehicleWheels)
            {
                    wheel.UpdateWheel(0f, .1f, 0f,false);
            }
        }
    }
    public void TrailerBrakes(bool onOff) => TrailerBrake = onOff;
}

