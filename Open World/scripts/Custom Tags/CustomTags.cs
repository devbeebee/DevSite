using System.Collections.Generic;
using UnityEngine;

public class CustomTags : MonoBehaviour
{
    public string defaultTag = "None";
    public List<string> m_tagList = new List<string>();
    private void Awake()=> m_tagList = new List<string>();
    void Start()
    {
        m_tagList.Add(defaultTag);
        for (var i = 0; i < m_tagList.Count; i++)
        {
            var tag = m_tagList[i];
            m_tagList[i] = tag.Trim().ToLower();
        }
        UpdateTagSystem();
    }

    public void EditDefaultStartupAndEditor(string newTag) => defaultTag = newTag;
    public void EditDefaultRuntime(string newTag) => m_tagList[0] = newTag;
    public void AddTag(string toAdd)
    {
        var tag = toAdd.ToLower();
        if (!m_tagList.Contains(tag))
        {
            RemoveFromTagSystem();
            m_tagList.Add(tag);
            UpdateTagSystem();
        }
    }

    public void RemoveTag(string toRemove)
    {
        var tag = toRemove.ToLower();
        if (m_tagList.Contains(tag))
        {
            RemoveFromTagSystem();
            m_tagList.Remove(tag);
            UpdateTagSystem();
        }
    }

    public List<string> GetTags()=> m_tagList;
    void UpdateTagSystem()=>CustomTagSystem.AddObject(this);
    void RemoveFromTagSystem()=> CustomTagSystem.RemoveObject(this);  
}
