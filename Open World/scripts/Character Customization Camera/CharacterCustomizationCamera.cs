using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum CameraPositionNames { Head ,Body }
[System.Serializable]
public class CamPos
{
    public string Name => cameraPositionName.ToString();
    public CameraPositionNames cameraPositionName;
    public Vector3 pos;
    public Vector3 rot;
}
public class CharacterCustomizationCamera : MonoBehaviour
{
    public bool cooldown = false;
    public float waitTime = 3f;

    public List<CamPos> positions;
    public void ChangeFromIndex(int index) => ChangeCamera( positions[index]);
    public void ChangeCamera(CamPos cp)
    {
transform.DOMove(cp.pos, waitTime).OnComplete(()=> cooldown = false);
        transform.DORotate(cp.rot, waitTime);
    }

}
