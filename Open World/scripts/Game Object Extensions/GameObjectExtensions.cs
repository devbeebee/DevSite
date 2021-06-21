using UnityEngine;

public static class GameObjectExtensions
{
    public static Vector3 GetLocalPosition(this GameObject go) => go.transform.localPosition;
    public static Vector3 GetEulerAngles(this GameObject go) => go.transform.eulerAngles;
    public static Vector3 GetLocalEulerAngles(this GameObject go) => go.transform.localEulerAngles;
    public static Vector3 GetRight(this GameObject go) => go.transform.right;
    public static Vector3 GetUp(this GameObject go) => go.transform.up;
    public static Vector3 GetForward(this GameObject go) => go.transform.forward;
    public static Quaternion GetRotation(this GameObject go) => go.transform.rotation;
    public static Vector3 GetPosition(this GameObject go) => go.transform.position;
    public static Quaternion GetLocalRotation(this GameObject go) => go.transform.localRotation;
    public static int ChilGetdCount(this GameObject go) => go.transform.childCount;
    public static Vector3 GetLocalScale(this GameObject go) => go.transform.localPosition;



    public static void SetPosition(this GameObject go, Vector3 pos) => go.transform.position = pos;
    public static void SetLocalPosition(this GameObject go, Vector3 pos) => go.transform.localPosition = pos;
    public static void SetEulerAngles(this GameObject go, Vector3 Euler) => go.transform.eulerAngles = Euler;
    public static void SetLocalEulerAngles(this GameObject go, Vector3 Euler) => go.transform.localEulerAngles = Euler;
    public static void SetParent(this GameObject go, Transform parent) => go.transform.parent = parent == null ? parent : null;

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component=>gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
    public static GameObject CloneObject(this GameObject go, Vector3 position, Quaternion rotation, Transform parent= null) => Object.Instantiate(go,position,rotation, parent == null ? parent : null);
}