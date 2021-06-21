using UnityEngine;
using TMPro;

[RequireComponent(typeof(NewInput))]
public class NewInputTest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MessageText;

    private void Update()
    {
        string msg = $"A: {NewInput.Instance.RawA:f2}" +
            $"\nB: {NewInput.Instance.RawB:f2}" +
            $"\nX: {NewInput.Instance.RawX:f2}" +
            $"\nY: {NewInput.Instance.RawY:f2}" +
            $"\nStart Button: {NewInput.Instance.RawPause:f2}" +
            $"\nLeft Right: {NewInput.Instance.RawInput.x:f2} Forward Back: {NewInput.Instance.RawInput.y:f2}" +
            $"\nLeft Right: {NewInput.Instance.RawLook.x:f2} UpDown: {NewInput.Instance.RawLook.y:f2}" +
            $"\nLeft Trigger: {NewInput.Instance.RawAim:f2}" +
            $"\nRight Trig: {NewInput.Instance.RawFire:f2}";
        MessageText.SetText(msg);
    }
}
