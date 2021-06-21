using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = default;
    public static T Instance => instance;
    protected virtual void Awake()
    {
        if (instance == null) { instance = GetComponent<T>(); }
        else { Destroy(this); }
    }
}