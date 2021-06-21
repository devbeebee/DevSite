using UnityEngine;

public class VehicleDoorController : MonoBehaviour
{
    public VehicleController Vehicle;
    bool entered = false;
    private void Start()
    {
        if (Vehicle)
        {
            Vehicle.AddDoor(this);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (NewInput.Instance.RawY == 1 && !entered)
        {
            GetIn(other.transform);
        }
    }
    public void OpenDoor()
    {
        //TBA
    }
    public void GetOut()=> entered = false;
    
    public void GetIn(Transform player)
    {
        entered = true;
        Vehicle.GetInVehicle(player);
    }
}
