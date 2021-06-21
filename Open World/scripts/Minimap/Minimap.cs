using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Minimap : MonoBehaviour
{
    [SerializeField] Camera MapCam;
    private void Start() => ZoomOut();

    void ZoomIn() => MapCam.DOOrthoSize(350, 3);
    void ZoomOut() => MapCam.DOOrthoSize(750, 3).OnComplete(() => Wait());
    void Wait() => StartCoroutine(Countdown());

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(4);
        ZoomIn();
    }
}
