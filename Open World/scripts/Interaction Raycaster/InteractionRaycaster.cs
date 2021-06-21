using UnityEngine;
using TMPro;
public class InteractionRaycaster : CameraRaycaster
{
    [SerializeField] TextMeshProUGUI InteractionText;
    public Interaction TempInteraction;
    public Transform ObjectHolder;
    public bool CarryingObject;
    void Start() => cam = GetComponent<Camera>();
    void Update()
    {
        if (CarryingObject)
        {
            return;
        }
        if (ShootRay())
        {
            SetInteractiionName(currentHit.collider.gameObject);
        }
        else if (InteractionText.text.Length > 0)
        {
            InteractionText.text = string.Empty;
        }
        else if (TempInteraction)
        {
            TempInteraction = null;
        }
        if (TempInteraction)
        {
            /*
            if (NewInput.Instance.Interaction)
            {
                TempInteraction.DoInteraction();
            }
            */
        }
    }
    private void LateUpdate()
    {
        if (TempInteraction)
        {
            if (TempInteraction.CanMoveObject && NewInput.Instance.RawWheel > 0)
            {
                if (!CarryingObject)
                {
                    ObjectHolder.position = TempInteraction.transform.position;
                    CarryingObject = true;
                }
                else
                {
                    TempInteraction.MoveObject(ObjectHolder.position);
                }
            }
            else if (CarryingObject)
            {
                CarryingObject = false;
            }
        }
    }
    void SetInteractiionName(GameObject go)
    {
        TempInteraction = go.GetComponent<Interaction>();
        if (TempInteraction)
        {
            if (RangeFromObject <= TempInteraction.MinmumDistance)
            {
                InteractionText.SetText($"<size=18>E</size><size=22>)</size> <size=25><voffset=-0.12em>{TempInteraction.InteractionName}</voffset> </size>");
            }
            else
            {
                InteractionText.text = string.Empty;
            }
        }
        else
        {
            InteractionText.text = string.Empty;
        }
    }
}
