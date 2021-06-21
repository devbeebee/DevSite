using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AimAssist
{
    float radius;
    LayerMask mask;
    public AimAssist(float _radius, LayerMask _mask)
    {
        radius = _radius;
        mask = _mask;
    }
    public Vector3 GetClostestTarget(Vector3 pos)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, radius, mask);
        List<TargetObject> trans = new List<TargetObject>();
        if (hitColliders.Length > 0)
        {
            foreach (var hitCollider in hitColliders)
            {
                TargetObject tarObj = hitCollider.transform.GetComponent<TargetObject>();
                if (tarObj)
                {
                    if (tarObj.IsVisible)
                    {
                        trans.Add(tarObj);
                    }
                }
            }
            return trans.OrderBy(t => t.Distance(pos)).ToArray()[0].transform.position;
        }
        return Vector3.zero;
    }
}
