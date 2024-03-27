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
    [SerializeField]private AudioClip jump;
    [SerializeField] private AudioClip powerPickUp;
    [SerializeField] private AudioClip fall;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip stage;
    [SerializeField] private AudioSource _audioSource;
    private Dictionary<>
    public 

}
