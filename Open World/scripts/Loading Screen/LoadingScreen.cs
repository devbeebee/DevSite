using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LoadingScreen : Singleton<LoadingScreen>
{
    [SerializeField] TextMeshProUGUI LoadingSceneText;
    public List<UnityAction> unityEvents = new List<UnityAction>();
    public string a;
    public string b;
    protected override void Awake()
    {
        base.Awake();
        unityEvents = new List<UnityAction>();
    }
    public void Add(UnityAction unityEv)
    {
        unityEvents.Add(unityEv);
    }
    IEnumerator Start()
    {
        for (int i = 0; i < 10; i++)
        {
            LoadingSceneText .SetText($"Gathering Jobs Please wait....{i+1} / 10 {((float)i+1).Percentage(10)}\nTotal Jobs : {unityEvents.Count}");
            yield return new WaitForSeconds(1);
        }
        for (int i = 0; i < unityEvents.Count; i++)
        {
            unityEvents[i].Invoke(); 
            yield return null;            
        }
        LoadingSceneText.SetText("Finished\nPress any Key to continue...");
        unityEvents = new List<UnityAction>();
    }
}