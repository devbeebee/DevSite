using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioClip[] AudioClips;
    [SerializeField] List<string> AudioClipNames = new List<string>();
    AudioSource audioSource;
    
    protected override void Awake() => base.Awake();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioClips = Resources.FindObjectsOfTypeAll(typeof(AudioClip)) as AudioClip[];
        foreach (var item in AudioClips)
        {
            AudioClipNames.Add(item.name);
        }
        if (AudioClips.Length > 0)
        {
            SetAudioClipAndPlay(audioSource, "AronChupa, Little Sis Nora - Rave in the Grave");
        }
    }
    
    public void SetAudioClipAndPlay(AudioSource aSource, string clipname)
    {
        SetAudioClip(aSource, clipname);
        audioSource.Play();
    }

    public void SetAudioClip(AudioSource aSource, string clipname)
    {
        if (AudioClipNames.Contains(clipname))
        {
            aSource.clip = AudioClips[AudioClipNames.IndexOf(clipname)];
        }
    }
}
