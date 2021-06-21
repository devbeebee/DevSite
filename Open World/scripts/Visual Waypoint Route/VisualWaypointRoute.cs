using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VisualWaypointRoute : MonoBehaviour
{
    LineRenderer line;

    private void OnValidate()
    {
        if (!line)
        {
            line = GetComponent<LineRenderer>();
        }
        if (transform.childCount > 1)
        {
            line.positionCount = transform.childCount;

            for (int i = 0; i < transform.childCount; i++)
            {
                line.SetPosition(i, transform.GetChild(i).position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
