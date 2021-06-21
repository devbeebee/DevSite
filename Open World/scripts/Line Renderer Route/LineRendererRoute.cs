using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererRoute : MonoBehaviour
{
    public LineRenderer line;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
        line.alignment = LineAlignment.TransformZ;
        transform.eulerAngles = new Vector3(90, 0, 0);
    }
    public void SetPositions(Vector3[] points, bool animate)
    {
        if (animate)
        {
            StartCoroutine(LineAnimator(points));
        }
        else
        {
            LinePos(points);
        }
    }

    IEnumerator LineAnimator(Vector3[] points)
    {
        line.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            line.SetPosition(i, points[i]);
            if (i % 2 == 0)
            {
                yield return null;
            }
        }
    }
    void LinePos(Vector3[] points)
    {
        line.positionCount = points.Length;
        line.SetPositions(points);        
    }
}
