using UnityEngine;

public class SingletonDontDestroyOnLoad<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T instance = default;
    public static T Instance => instance;

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("A instance already exists");
            Destroy(this);
            return;
        }
        else
        {
            instance = GetComponent<T>();
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
