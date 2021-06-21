using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerLandingGear : MonoBehaviour
{
    public Transform landingGear;
    public float speedTranslate; //Platform travel speed
    public Vector3 maxY; //The maximum height of the platform
    public Vector3 minY; //The minimum height of the platform
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {

        if (NewInput.Instance.RawDPadUp == 1)
        {
            Debug.Log("HERE UP" +
                "");
            landingGear.transform.localPosition = Vector3.MoveTowards(landingGear.transform.localPosition, maxY, speedTranslate * Time.deltaTime);
        }
        if (NewInput.Instance.RawDPadDown == 1)
        {
            Debug.Log("HERE DOWN");
            landingGear.transform.localPosition = Vector3.MoveTowards(landingGear.transform.localPosition, minY, speedTranslate * Time.deltaTime);
        }
        
    }
}
