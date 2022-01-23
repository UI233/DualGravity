using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }
    private static AudioManager _instance;

    [SerializeField]
    private AudioSource audioSource;

    public AudioClip combo;
    public AudioClip crash_stone;
    public AudioClip death;
    public AudioClip eat_right;
    public AudioClip eat_wrong;
    public AudioClip finish_target;
    public AudioClip fracture;
    public AudioClip health_up;
    public AudioClip vanish;

    /// <summary>
    /// ≤•∑≈“Ù–ß
    /// </summary>
    /// <param name="clip"></param>
    public void AudioPlay(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
