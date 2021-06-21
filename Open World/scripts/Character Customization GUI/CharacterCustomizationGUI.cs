using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CharacterCustomizationGUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NameSexText;
    [SerializeField] GameObject MainGUI;
    [SerializeField] GameObject HeadGUI;
    [SerializeField] CharacterCustomizationCamera CreationCamera;
    
    private void Start()
    {
        GotoMain();
    }
    private void Update()
    {
        if (MainGUI.activeSelf){ return; }
        if ( NewInput.Instance.RawB == 1 && !CreationCamera.cooldown)
        {
            GotoMain();
        }
    }
    void SetNameSexText(string msg)
    {
        NameSexText.SetText(msg);
    }
    public void GotoHead()
    {
        CreationCamera.ChangeFromIndex(1);
        MainGUI.SetActive(false);
        HeadGUI.SetActive(true);
    }
    public void GotoBody()
    {
        CreationCamera.ChangeFromIndex(2);
        MainGUI.SetActive(false);
        HeadGUI.SetActive(true);
    }
    public void GotoMain()
    {
        CreationCamera.ChangeFromIndex(0);
        HeadGUI.SetActive(false);
        MainGUI.SetActive(true);
    }
    public void GotoFirstperson()
    {
        CreationCamera.ChangeFromIndex(3);
        HeadGUI.SetActive(false);
        MainGUI.SetActive(false);
    }

}
