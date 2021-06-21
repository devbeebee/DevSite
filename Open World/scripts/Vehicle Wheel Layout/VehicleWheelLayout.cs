using UnityEngine;

public class VehicleWheelLayout :  WheelCollider
{
    public Transform WheelShape;
    public bool ApplySteering;
    public bool ApplyTorque;
    public float WheelRPM => rpm;
    public void UpdateWheel(float angle, float torque, float brake, bool handBrake)
    {
        if (ApplySteering)
        {
            this.steerAngle = angle;
        }
        if (handBrake == true)
        {
            brake = 1000;
        }
        brakeTorque = brake * 1000;
        if (ApplyTorque)
        {
            motorTorque = torque;
        }

        if (WheelShape)
        {
            GetWorldPose(out Vector3 p, out Quaternion q);
            WheelShape.position = p;
            WheelShape.rotation = q;
        }
    }
    public void UpdateSuspention(Transform vehicleTransform, Rigidbody vehiclebody, float naturalFrequency = 10, float dampingRatio = 0.8f, float forceShift = 0.03f, bool setSuspensionDistance = true)
    {
        JointSpring spring = suspensionSpring;

        spring.spring = Mathf.Pow(Mathf.Sqrt(sprungMass) * naturalFrequency, 2);
        spring.damper = 2 * dampingRatio * Mathf.Sqrt(spring.spring * sprungMass);

        suspensionSpring = spring;

        Vector3 wheelRelativeBody = vehicleTransform.InverseTransformPoint(transform.position);
        float distance = vehiclebody.centerOfMass.y - wheelRelativeBody.y + radius;

        forceAppPointDistance = distance - forceShift;

        // the following line makes sure the spring force at maximum droop is exactly zero
        if (spring.targetPosition > 0 && setSuspensionDistance)
        {
            suspensionDistance = sprungMass * Physics.gravity.magnitude / (spring.targetPosition * spring.spring);
        }
    }
}
