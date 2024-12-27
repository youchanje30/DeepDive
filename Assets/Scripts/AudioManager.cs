using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Linq;
using UnityEngine.UIElements;

public enum Sfx { button, hammer, reroll, buy };
public class AudioManager : SingleTone<AudioManager>
{

    [Serializable]
    public class AudioGroup
    {
        public AudioClip[] audioClips;    
    }
    public AudioGroup[] audioGroups;
    public Queue<AudioSource> audioSources = new Queue<AudioSource>();
    public Queue<AudioSource> playingSources = new Queue<AudioSource>();

    public void AddSfx()
    {
        while(playingSources.Count > 0 && !playingSources.Peek().isPlaying)
        {
            audioSources.Enqueue(playingSources.Dequeue());
        }
    }
    
    public void PlaySfx(Sfx sfx)
    {
        var speaker = GetSfxSource();
        speaker.clip = GetClip((int)sfx);
        speaker.Play();
    }

    public AudioClip GetClip(int sfx)
    {
        return audioGroups[sfx].audioClips[UnityEngine.Random.Range(0, audioGroups[sfx].audioClips.Length)];
    }

    public AudioSource GetSfxSource()
    {
        if (audioSources.Count == 0)
        {
            AddSfx();
        }
        if (audioSources.Count != 0) return audioSources.Dequeue();

        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = transform;
        playingSources.Enqueue(sfxObject.AddComponent<AudioSource>());
        return sfxObject.GetComponent<AudioSource>();
    }


    // public void PlayBgm(bool isPlay)
    // {
    //     if(isPlay)
    //     {
    //         bgmPlayer.Play();
    //     }
    //     else
    //     {
    //         bgmPlayer.Stop();
    //     }

    // }

    // public void ToggleBGM()
    // {
    //     bgmPlayer.mute = !bgmPlayer.mute;
    // }

    // public void ToggleSFX()
    // {
    //     for (int i = 0; i < sfxPlayers.Length; i++)
    //     {
    //         sfxPlayers[i].mute = !sfxPlayers[i].mute;
    //     }
    // }
    
    // public void ChangeSound(int index, float val)
    // {
    //     if(index == 0)
    //     {
    //         bgmPlayer.volume = val;
    //     }
    // }
}
