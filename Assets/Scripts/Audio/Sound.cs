using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;

    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume;
    [Range(0.1f, 3f)]
    public float Pitch;
    public float StartSeconds;
    public float EndSeconds;

    [HideInInspector]
    public AudioSource Source;

    public void PlaySound()
    {
        if (EndSeconds <= 0)
            EndSeconds = Clip.length;

        Source.time = StartSeconds;
        Source.Play();
        Source.SetScheduledEndTime(AudioSettings.dspTime + (EndSeconds - StartSeconds));
    }
}
