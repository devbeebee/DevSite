using UnityEngine;

public enum InteractionType { Item , NPC }

public class Interaction : MonoBehaviour
{
    public InteractionType TypeOfInteraction;
    public string InteractionName;
    public bool CanMoveObject = false;

    public float MinmumDistance = 5f;

    public bool MovingObject;

    public void DoInteraction()
    {
        switch (TypeOfInteraction)
        {
            case InteractionType.Item:
                Debug.Log("Picking Up Item");
                break;
            case InteractionType.NPC:
                Debug.Log("Speaking to NPC");
                break;
        }
    }

    public void MoveObject(Vector3 pos)=> transform.position = pos;
    
}