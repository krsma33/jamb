using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] Sounds;
    public Music[] Musics;

    void Awake()
    {
        AddMusics();
        AddSounds();
    }

    private void AddSounds()
    {
        foreach (var sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
        }
    }

    private void AddMusics()
    {
        foreach (var music in Musics)
        {
            music.Source = gameObject.AddComponent<AudioSource>();
            music.Source.clip = music.Clip;
            music.Source.volume = music.Volume;
            music.Source.pitch = music.Pitch;
            music.Source.loop = music.Loop;
        }
    }

    public void PlaySfx(string name)
    {
        var sound = Array.Find(Sounds, s => s.Name == name);
        sound.PlaySound();
    }

    public void PlayMusic(string name)
    {
        var music = Array.Find(Musics, m => m.Name == name);
        music.Play();
    }



}
