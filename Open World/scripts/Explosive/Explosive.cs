using UnityEngine;

public class Explosive : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public float lift = 30;
    public float fuse = 10;
    public bool explode = false;

    private void Update()
    {
        fuse -= Time.deltaTime;
        if (fuse < 0)
        {
            explode = true;
        }
    }
    void FixedUpdate()
    {                                             
        if (explode)
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                if (hit.attachedRigidbody)
                {
                    hit.attachedRigidbody.AddExplosionForce(power, explosionPos, radius, lift,ForceMode.Impulse);
                }
            }
            Destroy(gameObject,5f);
        }
    }
}
