using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AudioData", menuName = "AudioData", order = 2)]
public class AudioData : ScriptableObject
{
    public AudioType audioType;
    public MusicType musicType;
    public AudioClip audioClip;
}
