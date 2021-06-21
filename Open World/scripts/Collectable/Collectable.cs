using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableObject collectableData;
    [SerializeField] bool collect;
    private void Start()
    {
        Collectables.Instance.AddToMaster(collectableData,gameObject);
    }
    private void Update()
    {
        if (collect)
        {
            Collectables.Instance.AddFound(collectableData);
            gameObject.SetActive(false);
        }
    }
}
