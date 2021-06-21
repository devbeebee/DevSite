using UnityEngine;

public class PauseGame
{
    private float DefaultTimeScale;
    public PauseGame(float defaultScale)=> DefaultTimeScale = defaultScale;   
    public void Pause()=>  Time.timeScale = 0; 
    public void UnPause() => Time.timeScale = DefaultTimeScale;
}
