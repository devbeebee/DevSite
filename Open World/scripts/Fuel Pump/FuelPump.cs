using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

class FuelPump : FuelSystem
{
    [SerializeField]FuelStation station;
    public int PumpIndex = 0;
    public TextMeshPro PumpNumber;
    public TextMeshPro FilledPrice;
    public TextMeshPro FilledFuel;

    public bool ShouldBeFilling;
    public bool NozzleLifted;
    public bool NozzleReplaced;

    public bool BillSent;
    public TMP_Dropdown sceneNames;
    private void OnValidate()
    {

        PumpIndex = transform.GetSiblingIndex();
        name = $"Pump {PumpIndex+1}";
        if (PumpNumber) { PumpNumber.SetText($"{PumpIndex + 1}"); }
     
       // sceneNames.options = new List<TMP_Dropdown.OptionData>();
        //var regex = new Regex(@"([^/]*/)*([\w\d\-]*)\.unity");
       /*
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var path = SceneUtility.GetScenePathByBuildIndex(i);
            var name = regex.Replace(path, "$2");
            sceneNames.options.Add(new TMP_Dropdown.OptionData(name));
        }
        */
    }
    private void Start()
    {
        station = GetComponentInParent<FuelStation>();
        PumpIndex = transform.GetSiblingIndex();

    }
    private void Update()
    {
        if (ShouldBeFilling && NozzleLifted)
        {
            FillTank(Time.deltaTime);
            FilledPrice.SetText($"{GetCost():f2}");
            FilledFuel.SetText($"{filledAtPump:f2}");
        }
        if(!BillSent && NozzleReplaced)
        {
            SendBillToFuelStation();
            BillSent = true;
        }
    }

    void SendBillToFuelStation()=> station.AddBill( new FuelBill($"{GetCost():f2}"));
    
    public bool FillTank(float fillspeed)
    {
           FuelTank += fillspeed;
        filledAtPump += fillspeed;
        if (FuelTank < FuelTankCapacity) { return true; }
        return false;
    }
}
