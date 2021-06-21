using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum CollectableTypes { OnTrigger,Shoot}
public class Collectables : Singleton<Collectables>
{
    [System.Serializable]
    public class CollectableList
    {
        public CollectableList(CollectableTypes colType)
        {
             collectableType= colType;
        }
        public CollectableTypes collectableType;
        public List<string> CollectablesMaster = new List<string>();
        public List<string> CollectablesFound = new List<string>();
        public void AddToMaster(string collectableData) => CollectablesMaster.Add(collectableData);
        public void AddFound(string collectable) => CollectablesFound.Add(collectable);
    }

    public List<GameObject> CollectableGameObjects = new List<GameObject>();
    public CollectableList OnTriggerCollectables = new CollectableList(CollectableTypes.OnTrigger);
    public CollectableList ShootableCollectables = new CollectableList(CollectableTypes.Shoot);
    protected override void Awake() => base.Awake();

    private IEnumerator Start()
    {
        Debug.Log(Saveables.CollectablesJsonPath);
        for (int i = 0; i < 10; i++)
        {
            Debug.Log($"{i} / 10 ");
            yield return new WaitForSeconds(1);
        }

        CollectableList foundOnTrig = Saveables.LoadCollectables(CollectableTypes.OnTrigger);
        CollectableList foundOnShoot = Saveables.LoadCollectables(CollectableTypes.Shoot);

        for (int i = 0; i < CollectableGameObjects.Count; i++)
        {
            Collectable collectable = CollectableGameObjects[i].GetComponent<Collectable>();

            for (int j = 0; j < foundOnTrig.CollectablesFound.Count; j++)
            {
                if (foundOnTrig.CollectablesFound[j] == collectable.collectableData.CollectableName)
                {
                    OnTriggerCollectables.AddFound(foundOnTrig.CollectablesFound[j]);
                    CollectableGameObjects[i].SetActive(false);
                }
            }
            for (int j = 0; j < foundOnShoot.CollectablesFound.Count; j++)
            {
                if (foundOnShoot.CollectablesFound[j] == collectable.collectableData.CollectableName)
                {
                    ShootableCollectables.AddFound(foundOnShoot.CollectablesFound[j]);
                    CollectableGameObjects[i].SetActive(false);
                }
            }
        }

        for (int i = 0; i < CollectableGameObjects.Count; i++)
        {
            if (!CollectableGameObjects[i].activeSelf)
            {
                Destroy(CollectableGameObjects[i]);
            }
        }
        CollectableGameObjects=null;
    }

    public void AddToMaster(CollectableObject collectableData ,GameObject collectableGameobject)   
    {
        switch (collectableData.collectableType)
        {
            case CollectableTypes.OnTrigger: OnTriggerCollectables.AddToMaster(collectableData.CollectableName);  break;
            case CollectableTypes.Shoot: ShootableCollectables.AddToMaster(collectableData.CollectableName); break;
        }
        CollectableGameObjects.Add(collectableGameobject);
    }

    public void AddFound(CollectableObject collectableData) 
    {
        switch (collectableData.collectableType)
        {
            case CollectableTypes.OnTrigger: OnTriggerCollectables.AddFound(collectableData.CollectableName); Saveables.SaveCollectables(OnTriggerCollectables); break;
            case CollectableTypes.Shoot: ShootableCollectables.AddFound(collectableData.CollectableName); Saveables.SaveCollectables(ShootableCollectables); break;
        }
    }


    public string GetPercent(CollectableTypes collectableType)
    {
        switch (collectableType)
        {
            case CollectableTypes.OnTrigger: return $"{((float)OnTriggerCollectables.CollectablesFound.Count).Percentage(OnTriggerCollectables.CollectablesMaster.Count):f2}%"; ;
            case CollectableTypes.Shoot: return $"{((float)ShootableCollectables.CollectablesFound.Count).Percentage(ShootableCollectables.CollectablesMaster.Count):f2}%"; ;
        }
        return $"%Error%"; ;
    }
    public string GetOverallPercent()
    {
        int found = 0;
        found += OnTriggerCollectables.CollectablesFound.Count;
        found += ShootableCollectables.CollectablesFound.Count;
        
        int total = 0;
        total += OnTriggerCollectables.CollectablesMaster.Count;
        total += ShootableCollectables.CollectablesMaster.Count;

        return $"{((float)found).Percentage(total):f2}%";
    }

}
