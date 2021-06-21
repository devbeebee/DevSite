using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncOperationProgressExample : MonoBehaviour
{
    [SerializeField] string text;
    [SerializeField] bool opFin = true;
    [SerializeField] bool opFinlo = true;
    [SerializeField] AsyncOperation asyncUnload;
    Scene scene;
    private void Start()
    {
        scene = SceneManager.GetSceneAt(0);
    }
    private void Update()
    {
        if (NewInput.Instance.RawAim == 1)
        {
            StartCoroutine(loadScene());
        }
    }
    IEnumerator loadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scene.buildIndex, LoadSceneMode.Additive);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            opFin = async.isDone;
            text = async.progress + "";
            yield return null;
        }
        opFin = async.isDone;

    }
}
