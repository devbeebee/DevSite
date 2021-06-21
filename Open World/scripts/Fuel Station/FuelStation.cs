using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class FuelBill
{
    public string Bill;

    public FuelBill(string bill)
    {
        Bill = bill;
    }
}
public class FuelStation : MonoBehaviour
{
    public List<FuelBill> FuelBills;
    private void Start()
    {
        FuelBills = new List<FuelBill>();
    }
    public void AddBill(FuelBill bill) => FuelBills.Add(bill);
    public void PayBill(FuelBill bill) => FuelBills.Remove(bill);
}
