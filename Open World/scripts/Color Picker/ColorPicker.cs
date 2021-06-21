using UnityEngine;

[System.Serializable]
public class ColorPicker 
{
    public Color colorPicked => myGradient.Evaluate(colorPicker);
    public Gradient myGradient;
    [Range(0f, 1f)]
    public float colorPicker = 0.5f;
    [Range(0f, 1f)]
    public float colorAlpha = 0.5f;

    public ColorPicker()
    {
        myGradient = new Gradient();

        GradientColorKey[] colorKey = new GradientColorKey[3];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.green;
        colorKey[1].time = 0.5f;
        colorKey[2].color = Color.blue;
        colorKey[2].time = 1.0f;

        GradientAlphaKey[] alphaKey = new GradientAlphaKey[3];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha =1f;
        alphaKey[1].time = 0.5f;
        alphaKey[2].alpha = 1f;
        alphaKey[2].time = 1.0f;

        myGradient.SetKeys(colorKey, alphaKey);
    }
}
