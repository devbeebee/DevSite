using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Base Vechile", menuName = "ScriptableObjects/Vechile/BaseVechile", order = 0)]
public class BaseVehicleScriptable : ScriptableObject
{
    public string itemName;
    public GameObject wheelShape;
    public float maxAngle = 30;
    public float maxTorque = 300;
    public AudioClip VehicleHorn;
    public TransmittionType transmittion = TransmittionType.Auto;
    public List<float> Gears = new List<float>();
    public  int TotalGears()
    {
        return 0;
    }
    public float GetTorque(float engineRPM, int currentGear =0)
    {
        switch (transmittion)
        {
            case TransmittionType.Auto:
                break;
            case TransmittionType.Manual:
                break;
            default:
                break;
        }
        return 0;
    }
    float Auto() { return 0; }
    float Manual() { return 0; }

    public float GetGear(int currentGear) => Gears[currentGear];
    public int GetTotalGears => Gears.Count;
}
