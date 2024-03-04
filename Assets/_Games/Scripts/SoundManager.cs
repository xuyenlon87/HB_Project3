﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : Singleton<SoundManager>
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip click;
    public AudioClip die;
    public AudioClip backgroundMusic;
    public AudioClip attack;
    public AudioClip onHit;


    public void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }

    public void PlaySound(AudioClip sound)
    {
        sfxSource.PlayOneShot(sound);
    }
    public void PlaySoundAt(AudioClip sound, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(sound, position);
    }
}
