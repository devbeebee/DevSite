using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DriveType { AWD, RWD, FWD }
public enum SpeedValueType { KPH, MPH }
public enum TransmittionType { Auto, Manual }
public class VehicleController : MonoBehaviour
{
    public BaseVehicleScriptable VehicleScriptable;

    public void AddDoor(VehicleDoorController door)
    {
        if (VehicleDoors != null)
        {
            VehicleDoors.Add(door);
        }
    }

    public AudioSource EngineAudioSource;
    public AudioSource HornAudioSource;
    public AudioSource RadioAudioSource;

    public List<Transform> CameraPositions;
    public List<VehicleWheelLayout> VehicleWheels;
    List<VehicleDoorController> VehicleDoors;
    private Light[] lightsLeft;
    private Light[] lightsRight;
    private Light[] headlights;
    private Light[] brakelights;

    Transform VehicleTransform;
    Rigidbody VehicleBody;
    public float MaxEngineRPM = 3000.0f;
    public float MinEngineRPM = 1000.0f;
    [SerializeField]
    private float WheelsAvgRPM = 0.0f;
    [SerializeField]
    private float EngineRPM = 0.0f;
    public int currentGear;
    public float[] GearRatio;
    public bool handBrake;

    public float angle;
    public float torque;
    public float brake;
    public float VechileSpeedKPH = 0;
    public float VechileSpeedMPH = 0;
    public TransmittionType Transmittion;
    public DriveType DriveTypeValue;
    public SpeedValueType SpeedValue;
    [Range(0, 20)]
    public float naturalFrequency = 10;
    [Range(0, 3)]
    public float dampingRatio = 0.8f;
    [Range(-1, 1)]
    public float forceShift = 0.03f;
    public bool setSuspensionDistance = true;
    public bool isBeingControlled = true;
    public Transform driverseat;
    public Transform Driver;

    public bool GettingIn = false;
    int activeCamera = 0;
    private bool swappingCamera;
    public bool stopped;
    public bool idling;

    public float drive = 1;
    private void Awake()
    {
        VehicleDoors = new List<VehicleDoorController>();
    }
    public void Start()
    {
        VehicleTransform = transform;
        VehicleBody = GetComponent<Rigidbody>();
        HornAudioSource.clip = VehicleScriptable.VehicleHorn;
        BuildWheels();
    }
    void EngineSound(float speed)
    {
        if (speed < .1f)
        {
            EngineAudioSource.pitch = .1f;
        }
        else
        {
            EngineAudioSource.pitch = speed / 100;
        }
    }
    public void BuildWheels()
    {
        if (VehicleScriptable.wheelShape != null)
        {
            for (int i = 0; i < VehicleWheels.Count; i++)
            {
                var wheel = VehicleWheels[i];
                if (wheel.transform.childCount > 0)
                {
                    DestroyImmediate(wheel.transform.GetChild(0).gameObject);
                }
            }
            for (int i = 0; i < VehicleWheels.Count; i++)
            {
                var wheel = VehicleWheels[i];
                var ws = GameObject.Instantiate(VehicleScriptable.wheelShape);
                wheel.WheelShape = ws.transform;
                ws.transform.parent = wheel.transform;
                wheel.UpdateWheel(0, 0, 0, false);

            }
        }
    }

    public void Update()
    {
        if (!Driver) { return; }

        if (NewInput.Instance.RawY == 1 && isBeingControlled)
        {
            GetOut();
        }
        if (NewInput.Instance.IsGamepad)
        {
            brake = NewInput.Instance.RawAim;
            torque = drive * NewInput.Instance.RawFire;
            if (NewInput.Instance.RawABool)
            {
                Debug.Log("handBrake");
                handBrake = !handBrake;
            }
        }
        else
        {
            torque = NewInput.Instance.GetMovementAxis(InputAxis.Y);
        }
        angle = NewInput.Instance.GetMovementAxis(InputAxis.X);
        //     brake = maxTorque * NewInput.Instance.LeftTrigger;
        float rpmSum = 0;
        foreach (VehicleWheelLayout wheel in VehicleWheels)
        {
            //float torque = TorqueWithGears(1);
            wheel.UpdateWheel(VehicleScriptable.maxAngle * angle, VehicleScriptable.maxTorque * torque, brake, handBrake);
            wheel.UpdateSuspention(VehicleTransform, VehicleBody, naturalFrequency, dampingRatio, forceShift, setSuspensionDistance);
            rpmSum += wheel.WheelRPM;
        }
        WheelsAvgRPM = rpmSum / VehicleWheels.Count;
        float vel = 0;
        EngineRPM = Mathf.SmoothDamp(EngineRPM, 1000 + (Mathf.Abs(WheelsAvgRPM) * 3.0f * (VehicleScriptable.GetGear(currentGear))), ref vel, Time.smoothDeltaTime);
        if (EngineRPM == 1000)
        {
            idling = true;
        }
        else
        {
            idling = false;
        }
        if (VechileSpeedKPH == 0 && handBrake)
        {
            stopped = true;
        }
        else
        {
            stopped = false;
        }
        if (stopped)
        {
            if (brake == 1)
            {
                drive = -1;
            }
            if (torque == 1 || torque == -1)
            {
                drive = 1;
            }
        }
        if (NewInput.Instance.RawBack == 1 && !swappingCamera)
        {
            SwapCamera();
        }
        else if (NewInput.Instance.RawBack == 0 && swappingCamera)
        {
            swappingCamera = false;
        }
        Speed();
    }

    private void GetOut()
    {
        PlayerCamera.Instance.ResetCamera();
        isBeingControlled = false;
        GettingIn = false;
        Rigidbody rb = Driver.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        Driver.parent = null;
        Driver = null;
        PlayerManager.Instance.IsInVehicle = false;
        foreach (var item in VehicleDoors)
        {
            item.GetOut();
        }
    }

    public void GetInVehicle(Transform driver)
    {
        if (GettingIn) return;
        PlayerCamera.Instance.MoveCameraAndReParent(CameraPositions[0]);
        PlayerManager.Instance.IsInVehicle = true;
        GettingIn = true;
        Driver = driver;
        StartCoroutine(SitAnim());
        Rigidbody rb = driver.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        driver.parent = driverseat;
        driver.localPosition = Vector3.zero;
    }
    IEnumerator SitAnim()
    {
        yield return new WaitForSeconds(3);
        isBeingControlled = true;
        GettingIn = false;
    }
    void SwapCamera()
    {
        swappingCamera = true;
        activeCamera++;
        if (activeCamera > CameraPositions.Count - 1)
        {
            activeCamera = 0;
        }
        PlayerCamera.Instance.MoveCameraAndReParent(CameraPositions[activeCamera]);

    }

    float TorqueWithGears(int gear)
    {
        return 0;
    }


    void Speed()
    {
        VechileSpeedKPH = VehicleBody.velocity.magnitude * 3.6f;
        VechileSpeedMPH = VechileSpeedKPH / 1.609f;

        if (VechileSpeedKPH < 0.1f)
        {
            VechileSpeedKPH = 0f;
        }
        EngineSound(VechileSpeedKPH);
    }
    void Horn()
    {
        if (NewInput.Instance.RawH > 0)
        {
            if (!HornAudioSource.isPlaying)
            {
                HornAudioSource.loop = true;
                HornAudioSource.Play();
            }
        }
        else
        {
            HornAudioSource.loop = false;
        }
    }

    public float AccelInput { get; private set; }
    public float BrakeInput { get; private set; }
    public float MaxSpeed = 35;

    private float m_SteerAngle;
    internal void Move(float steering, float accel, float footbrake, float handbrake)
    {
        m_SteerAngle = steering * VehicleScriptable.maxAngle;
        steering = Mathf.Clamp(steering, -1, 1);
        AccelInput = accel = Mathf.Clamp(accel, 0, 1);
        BrakeInput = footbrake = -1 * Mathf.Clamp(footbrake, -1, 0);
        handbrake = Mathf.Clamp(handbrake, 0, 1);

        foreach (VehicleWheelLayout wheel in VehicleWheels)
        {
            wheel.UpdateWheel(steering * VehicleScriptable.maxAngle, VehicleScriptable.maxTorque * accel, footbrake, handBrake);
            wheel.UpdateSuspention(VehicleTransform, VehicleBody, naturalFrequency, dampingRatio, forceShift, setSuspensionDistance);

        }
    }
}
