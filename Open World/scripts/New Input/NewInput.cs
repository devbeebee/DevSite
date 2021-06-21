using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public enum InputAxis { X, Y }
[RequireComponent(typeof(PlayerInput))]
public class NewInput : Singleton<NewInput>
{
    Mouse mouse;
    public bool IsGamepad;
    [Header("x= AD y= WS")]
    public Vector2 RawInput;
    [Header("Mouse x & y")]
    public Vector2 RawLook;
    [Header("Mouse x & y")]
    public Vector2 RawLookWp;
    [Header("Left Click")]
    public float RawFire;
    [Header("Right Click")]
    public float RawAim;
    [Header("Wheel Click")]
    public float RawWheel;
    [Header("Wheel Scroll")]
    public float RawWheelScrollFwd;
    [Header("H")]
    public float RawH;
    [Header("Inventory")]
    public float RawInventory;
    public float RawPause;
    public float RawBack;
    public float RawA;
    public bool RawABool;
    public float RawB;
    public float RawX;
    public float RawY;

    public float RawDPadUp;
    public float RawDPadDown;
    public float RawDPadLeft;
    public float RawDPadRight;

    protected override void Awake() => base.Awake();
    private void Start()
    {
        if (Gamepad.current!=null)
        {
            IsGamepad = true;
        }
    }
    public void OnPause(CallbackContext value)=> RawPause = Mathf.Abs(value.ReadValue<float>());
    public void OnBack(CallbackContext value)=> RawBack = Mathf.Abs(value.ReadValue<float>());
    public void OnA(CallbackContext value) { RawA = Mathf.Abs(value.ReadValue<float>()); OnATap(value);     }
    public void OnATap(CallbackContext value) 
    {
        if (RawA ==1)
        {
            Debug.Log($" Raw  a  {RawA} P");
            RawABool = true;
        }
        else { Debug.Log($" Raw  a  {RawA} else"); RawABool = false; }
      
    }
    public void OnB(CallbackContext value)=> RawB = Mathf.Abs(value.ReadValue<float>());
    public void OnX(CallbackContext value)=> RawX = Mathf.Abs(value.ReadValue<float>());
    public void OnY(CallbackContext value)=> RawY = Mathf.Abs(value.ReadValue<float>());
    public void OnDPadUp(CallbackContext value)=> RawDPadUp = Mathf.Abs(value.ReadValue<float>());
    public void OnDPadDown(CallbackContext value)=> RawDPadDown = Mathf.Abs(value.ReadValue<float>());
    public void OnDPadLeft(CallbackContext value)=> RawDPadLeft = Mathf.Abs(value.ReadValue<float>());
    public void OnDPadRight(CallbackContext value)=> RawDPadRight = Mathf.Abs(value.ReadValue<float>());
    public void OnMovement(CallbackContext value)=>RawInput = value.ReadValue<Vector2>();
    public void OnLook(CallbackContext value) 
    {
        RawLook = value.ReadValue<Vector2>(); 
        RawLookWp =Camera.main.ScreenToWorldPoint( value.ReadValue<Vector2>()); 
    }
    public void OnFire(CallbackContext value) => RawFire = Mathf.Abs(value.ReadValue<float>());
    public void OnAim(CallbackContext value) => RawAim = Mathf.Abs(value.ReadValue<float>());
    public void OnMouseWheelClick(CallbackContext value) => RawWheel = Mathf.Abs(value.ReadValue<float>());
    public void OnMouseWheelScroll(CallbackContext value) => RawWheelScrollFwd = Mathf.Clamp(value.ReadValue<float>(), -1, 1);

    public void OnHClick(CallbackContext value) => RawH = Mathf.Abs(value.ReadValue<float>());
    public void OnInventoryClick(CallbackContext value) => RawInventory = Mathf.Abs(value.ReadValue<float>());

    public float GetMouseAxis(InputAxis ia)
    {
        switch (ia)
        {
            case InputAxis.X:
                return RawLook.x;
            case InputAxis.Y:
                return RawLook.y;
        }
        return 0;
    }
    public float GetMovementAxis(InputAxis ia)
    {
        switch (ia)
        {
            case InputAxis.X:
                return RawInput.x;
            case InputAxis.Y:
                return RawInput.y;
        }
        return 0;
    }
}
