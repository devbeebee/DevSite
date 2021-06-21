using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshCollider))]
public class ObjectDamageable : MonoBehaviour
{
    public UnityEvent OnZeroHealth;
    [SerializeField] float ObjectDamage = 100;
    [SerializeField] bool DestroyOnZeroHealth = true;

    void Start()
    {
        if (DestroyOnZeroHealth)
        {
            if (OnZeroHealth == null)
            {
                OnZeroHealth = new UnityEvent();
            }
            OnZeroHealth.AddListener(DestroyMe);
        }
    }
    void DestroyMe() => Destroy(gameObject);

    public void TakeDamage(float damage)
    {
        ObjectDamage -= damage;
        if (ObjectDamage <= 0)
        {
            if (OnZeroHealth != null)
            {
                OnZeroHealth.Invoke();
            }
        }
    }
}