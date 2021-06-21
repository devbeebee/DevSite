using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
    }
    public List<AxleInfo> axleInfos;

    //Car data
    public float maxMotorTorque = 500f;
    public float maxSteeringAngle = 40f;

    //To get a more realistic behavior
    public Vector3 centerOfMassChange;

    //The difference between the center of the car and the position where we steer
    public float centerSteerDifference;
    //The position where the car is steering
    public Vector3 steerPosition;

    //All waypoints
    public List<Transform> allWaypoints;
    //The current index of the list with all waypoints
    public int currentWaypointIndex = 0;
    //The waypoint we are going towards and the waypoint we are going from
    private Vector3 currentWaypoint;
    private Vector3 previousWaypoint;

    //Average the steering angles to simulate the time it takes to turn the wheel
    public float averageSteeringAngle = 0f;

    public PIDController PIDControllerScript;

    void Start()
    {

        //Init the waypoints
        currentWaypoint = allWaypoints[currentWaypointIndex].position;

        previousWaypoint = GetPreviousWaypoint();

    }


    void Update()
    {
        //So we can experiment with the position where the car is checking if it should steer left/right
        //doesn't have to be where the wheels are - especially if we are reversing
        steerPosition = transform.position + transform.forward * centerSteerDifference;

        //Check if we should change waypoint
        if (steerPosition.HasPassedVector(previousWaypoint, currentWaypoint))
        {
            currentWaypointIndex += 1;

            if (currentWaypointIndex == allWaypoints.Count)
            {
                currentWaypointIndex = 0;
            }

            currentWaypoint = allWaypoints[currentWaypointIndex].position;

            previousWaypoint = GetPreviousWaypoint();
        }
    }

    //Get the waypoint before the current waypoint we are driving towards
    Vector3 GetPreviousWaypoint()
    {
        previousWaypoint = Vector3.zero;

        if (currentWaypointIndex - 1 < 0)
        {
            previousWaypoint = allWaypoints[allWaypoints.Count - 1].position;
        }
        else
        {
            previousWaypoint = allWaypoints[currentWaypointIndex - 1].position;
        }

        return previousWaypoint;
    }

    void FixedUpdate()
    {
        float motor = maxMotorTorque;
        float CTE = NothingButMath.GetCrossTrackError(steerPosition, previousWaypoint, currentWaypoint);
        CTE *= NothingButMath.SteerDirection(transform, steerPosition, currentWaypoint);

        float steeringAngle = PIDControllerScript.GetSteerFactorFromPIDController(CTE);
        steeringAngle = Mathf.Clamp(steeringAngle, -maxSteeringAngle, maxSteeringAngle);
        float averageAmount = 30f;
        averageSteeringAngle += ((steeringAngle - averageSteeringAngle) / averageAmount);

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = averageSteeringAngle;
                axleInfo.rightWheel.steerAngle = averageSteeringAngle;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

        }


    }
}