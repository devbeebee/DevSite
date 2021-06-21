using UnityEngine;

public class Binoculars : MonoBehaviour
{
    private void Update()
    {
        if (NewInput.Instance.RawWheelScrollFwd != 0)
        {
            Zoom((int)NewInput.Instance.RawWheelScrollFwd);
        }

    }
    public void Zoom(int dir)
    {
        if (dir > 0) { Debug.Log("In"); }
        if (dir < 0) { Debug.Log("Out"); }
    }
}
