using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MusicType
{
    None,
    BackGround
}
public enum AudioType
{
    None,
    Jump,
    PowerPickUp,
    Fall,
    Win,
    Stage
}
public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioData> audioDataList;
    [SerializeField] private AudioSource _audioSource;
    
    public void PlayMusic(MusicType type)
    {
        AudioData audioData = FindAudio(type);
        if (audioData.musicType!= MusicType.None)
        {
            _audioSource.clip = audioData.audioClip;
            _audioSource.Play();
        }
    }
    public void PlayMusic(AudioType type)
    {
        AudioData audioData = FindAudio(type);
        if(audioData.audioType!=AudioType.None)
        _audioSource.PlayOneShot(audioData.audioClip);
    }
    public void MusicOn()
    {
        _audioSource.mute = false;
    }
    public void MusicOff()
    {
        _audioSource.mute = true;
    }
    private AudioData FindAudio(AudioType audioType)
    {
        foreach(AudioData audioData in audioDataList)
        {
            if (audioData.audioType == audioType)
                return audioData;
        }
        return null;
    }
    private AudioData FindAudio(MusicType musicType)
    {
        foreach (AudioData audioData in audioDataList)
        {
            if (audioData.musicType == musicType)
                return audioData;
        }
        return null;
    }
}
