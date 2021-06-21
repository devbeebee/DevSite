using UnityEngine;

[CreateAssetMenu(fileName = "Base Collectable", menuName = "ScriptableObjects/Collectable", order = 0)]
public class CollectableObject : ScriptableObject
{
    public string CollectableName = "";
    public CollectableTypes collectableType;
}
