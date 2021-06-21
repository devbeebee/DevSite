using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CustomTags))]
public class PlayerManager : Singleton<PlayerManager>
{
    [Range(0, 360)] float x;
    public Experience PlayerExperience;
    public bool GamePaused=false;
    Rigidbody rbody;
    Vector3 targetVelocity;
    public float walkSpeed = 6;
    public float strafeSpeed = 5;
    public float gravity = 9.82f;

    public bool IsInVehicle = false;

    // Use this for initialization
    protected override void Awake() => base.Awake();
    void Start()
    {
        Quaternion rotation = UnityEngine.Quaternion.Euler(0, 30, 0);
        rbody = this.GetComponentInChildren<Rigidbody>();
        PlayerExperience.LoadData();
        StartCoroutine(Test());
    }
    IEnumerator Test()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            GainExp(5);
        }
    }
    void FixedUpdate()
    {
        if (GamePaused || IsInVehicle)
        {
            return;
        }
        float x = NewInput.Instance.GetMovementAxis(InputAxis.X);
        float y = NewInput.Instance.GetMovementAxis(InputAxis.Y);
        targetVelocity = new Vector3(x * strafeSpeed, 0, y * walkSpeed);
        targetVelocity = transform.TransformDirection(targetVelocity);
        Vector3 velocity = rbody.velocity;
        Vector3 velocityChange = targetVelocity - velocity;
        velocityChange.y = 0;
        rbody.AddForce(velocityChange, ForceMode.VelocityChange);
        rbody.AddForce(new Vector3(0, -gravity * rbody.mass, 0));
    }
    public void GainExp(int xp)
    {
        PlayerExperience.GainExp(xp);
        PlayerExperience.SaveExperience();
    }
}
