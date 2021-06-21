using System.Collections;
using UnityEngine;

public enum FuelType { Petrol, Diesel }
public class FuelSystem : MonoBehaviour
{
    public FuelType TypeOfFuel;
    public float PetrolPrice = 100;
    public float DieselPrice = 100;
    public float FuelTank = 1000;
    public float FuelTankCapacity = 1000;
    public float filledAtPump = 0;

    public float GetCost()=> filledAtPump * Price();  
    float Price()
    {
        if (TypeOfFuel == FuelType.Petrol) return PetrolPrice;
        else return DieselPrice;
    }
   
}

