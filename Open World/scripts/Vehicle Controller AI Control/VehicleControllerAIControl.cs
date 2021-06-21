using UnityEngine;

public class VehicleControllerAIControl : MonoBehaviour
{
    [SerializeField] LineRenderer Line;
    public enum BrakeCondition { NeverBrake, TargetDirectionDifference, TargetDistance }
    [SerializeField] [Range(0, 1)] private float m_CautiousSpeedFactor = 0.05f;               // percentage of max speed to use when being maximally cautious
    [SerializeField] [Range(0, 180)] private float m_CautiousMaxAngle = 50f;                  // angle of approaching corner to treat as warranting maximum caution
    [SerializeField] private float m_CautiousMaxDistance = 100f;                              // distance at which distance-based cautiousness begins
    [SerializeField] private float m_CautiousAngularVelocityFactor = 30f;                     // how cautious the AI should be when considering its own current angular velocity (i.e. easing off acceleration if spinning!)
    [SerializeField] private float m_SteerSensitivity = 0.05f;                                // how sensitively the AI uses steering input to turn to the desired direction
    [SerializeField] private float m_AccelSensitivity = 0.04f;                                // How sensitively the AI uses the accelerator to reach the current desired speed
    [SerializeField] private float m_BrakeSensitivity = 1f;                                   // How sensitively the AI uses the brake to reach the current desired speed
    [SerializeField] private float m_LateralWanderDistance = 3f;                              // how far the car will wander laterally towards its target
    [SerializeField] private float m_LateralWanderSpeed = 0.1f;                               // how fast the lateral wandering will fluctuate
    [SerializeField] [Range(0, 1)] private float m_AccelWanderAmount = 0.1f;                  // how much the cars acceleration will wander
    [SerializeField] private float m_AccelWanderSpeed = 0.1f;                                 // how fast the cars acceleration wandering will fluctuate
    [SerializeField] private BrakeCondition m_BrakeCondition = BrakeCondition.TargetDistance; // what should the AI consider when accelerating/braking?
    [SerializeField] private bool m_Driving;                                                  // whether the AI is currently actively driving or stopped.
    [SerializeField] private Transform m_Target;                                              // 'target' the target object to aim for.
    [SerializeField] private bool m_StopWhenTargetReached;                                    // should we stop driving when we reach the target?
    [SerializeField] private float m_ReachTargetThreshold = 2;                                // proximity to target to consider we 'reached' it, and stop driving.

    private float m_RandomPerlin;             // A random value for the car to base its wander on (so that AI cars don't all wander in the same pattern)
    private VehicleController m_CarController;    // Reference to actual car controller we are controlling
    private float m_AvoidOtherCarTime;        // time until which to avoid the car we recently collided with
    private float m_AvoidOtherCarSlowdown;    // how much to slow down due to colliding with another car, whilst avoiding
    private float m_AvoidPathOffset;          // direction (-1 or 1) in which to offset path to avoid other car, whilst avoiding
    private Rigidbody m_Rigidbody;

    public Transform allWaypoints;
    public int currentWaypointIndex = 0;
    private Vector3 currentWaypoint;
    private Vector3 previousWaypoint;
    public Vector3 steerPosition;
    public float centerSteerDifference;


    private void Awake()
    {
        currentWaypoint = allWaypoints.GetChild(0).position;
        m_CarController = GetComponent<VehicleController>();
        m_RandomPerlin = Random.value * 100;

        m_Rigidbody = GetComponent<Rigidbody>();
        Line.positionCount = 2;
        if (m_Target == null)
        {
            GameObject go = new GameObject();
            m_Target = go.transform;
            go.transform.parent = null;
        }
    }

    private void LateUpdate()
    {
        Line.SetPosition(0, transform.position);
        Line.SetPosition(1, m_Target.position);
    }
    void Update()
    {
        m_Target.position = currentWaypoint;
        steerPosition = transform.position + transform.forward * centerSteerDifference;
        if (steerPosition.HasPassedVector(previousWaypoint, currentWaypoint))
        {
            currentWaypointIndex += 1;

            if (currentWaypointIndex == allWaypoints.childCount)
            {
                currentWaypointIndex = 0;
            }

            currentWaypoint = allWaypoints.GetChild(currentWaypointIndex).position;

            previousWaypoint = GetPreviousWaypoint();
        }
    }

    Vector3 GetPreviousWaypoint()
    {
        if (currentWaypointIndex - 1 < 0)
        {
            return allWaypoints.GetChild(allWaypoints.childCount - 1).localPosition;
        }
        else
        {
            return allWaypoints.GetChild(currentWaypointIndex - 1).localPosition;
        }
    }
    private void FixedUpdate()
    {
        if (m_Target == null || !m_Driving)
        {
            m_CarController.Move(0, 0, -1f, 1f);
        }
        else
        {
            Vector3 fwd = transform.forward;
            if (m_Rigidbody.velocity.magnitude > m_CarController.MaxSpeed * 0.1f)
            {
                fwd = m_Rigidbody.velocity;
            }

            float desiredSpeed = m_CarController.MaxSpeed;

            switch (m_BrakeCondition)
            {
                case BrakeCondition.TargetDirectionDifference:
                    {
                        float approachingCornerAngle = Vector3.Angle(m_Target.forward, fwd);
                        float spinningAngle = m_Rigidbody.angularVelocity.magnitude * m_CautiousAngularVelocityFactor;
                        float cautiousnessRequired = Mathf.InverseLerp(0, m_CautiousMaxAngle, Mathf.Max(spinningAngle, approachingCornerAngle));
                        desiredSpeed = Mathf.Lerp(m_CarController.MaxSpeed, m_CarController.MaxSpeed * m_CautiousSpeedFactor,
                                                  cautiousnessRequired);
                        break;
                    }

                case BrakeCondition.TargetDistance:
                    {
                        Vector3 delta = m_Target.position - transform.position;
                        float distanceCautiousFactor = Mathf.InverseLerp(m_CautiousMaxDistance, 0, delta.magnitude);
                        float spinningAngle = m_Rigidbody.angularVelocity.magnitude * m_CautiousAngularVelocityFactor;
                        float cautiousnessRequired = Mathf.Max(Mathf.InverseLerp(0, m_CautiousMaxAngle, spinningAngle), distanceCautiousFactor);
                        desiredSpeed = Mathf.Lerp(m_CarController.MaxSpeed, m_CarController.MaxSpeed * m_CautiousSpeedFactor, cautiousnessRequired);
                        break;
                    }

                case BrakeCondition.NeverBrake:
                    break;
            }

            Vector3 offsetTargetPos = m_Target.position;

            if (Time.time < m_AvoidOtherCarTime)
            {
                desiredSpeed *= m_AvoidOtherCarSlowdown;
                offsetTargetPos += m_Target.right * m_AvoidPathOffset;
            }
            else
            {
                offsetTargetPos += m_Target.right * (Mathf.PerlinNoise(Time.time * m_LateralWanderSpeed, m_RandomPerlin) * 2 - 1) * m_LateralWanderDistance;
            }

            float accelBrakeSensitivity = (desiredSpeed < m_CarController.VechileSpeedKPH) ? m_BrakeSensitivity : m_AccelSensitivity;
            float accel = Mathf.Clamp((desiredSpeed - m_CarController.VechileSpeedKPH) * accelBrakeSensitivity, -1, 1);
            accel *= (1 - m_AccelWanderAmount) + (Mathf.PerlinNoise(Time.time * m_AccelWanderSpeed, m_RandomPerlin) * m_AccelWanderAmount);
            Vector3 localTarget = transform.InverseTransformPoint(offsetTargetPos);
            float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
            float steer = Mathf.Clamp(targetAngle * m_SteerSensitivity, -1, 1) * Mathf.Sign(m_CarController.VechileSpeedKPH);
            m_CarController.Move(steer, accel, accel, 0f);
            if (m_StopWhenTargetReached && localTarget.magnitude < m_ReachTargetThreshold)
            {
                m_Driving = false;
            }
        }
    }
}