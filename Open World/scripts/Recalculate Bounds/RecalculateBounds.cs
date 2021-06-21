using UnityEngine;
public class RecalculateBounds : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] BoxCollider col;
    
    void Awake()
    {
        if (sr && col)
        {
            col.size = new Vector3(sr.bounds.size.x / transform.lossyScale.x, sr.bounds.size.y / transform.lossyScale.y, sr.bounds.size.z / transform.lossyScale.z);
        }
    }
}
