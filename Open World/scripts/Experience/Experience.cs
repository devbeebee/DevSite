using System;
using UnityEngine;


[System.Serializable]
public class Experience
{
    public int vLevel = 1;
    public int vCurrExp = 0;
    public int vExpBase = 10;
    public int vExpLeft = 10;
    public float vExpMod = 1.15f;

    public void LoadData()
    {
        Experience exp = Saveables.LoadExp();
        vLevel = exp.vLevel;
        vCurrExp = exp.vCurrExp;
        vExpBase = exp.vExpBase;
        vExpLeft = exp.vExpLeft;
        vExpMod = exp.vExpMod;
    }

    public void GainExp(int e)
    {
        vCurrExp += e;
        if (vCurrExp >= vExpLeft)
        {
            LvlUp();
        }
    }
    void LvlUp()
    {
        vCurrExp -= vExpLeft;
        vLevel++;
        float t = Mathf.Pow(vExpMod, vLevel);
        vExpLeft = (int)Mathf.Floor(vExpBase * t);
    }
}

[System.Serializable]
public class Fatigue
{ 

}

static class FloatExtensions
{
    public static float Percentage(this float current, float maximum) => (current / maximum) * 100;
}