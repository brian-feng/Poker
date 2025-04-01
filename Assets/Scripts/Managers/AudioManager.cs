using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.ExceptionServices;
using UnityEngine.UI;

/// <summary>
/// The manager that handles all sound effects that play. Very simplistically contains a function for every clip of audio that may be used.
/// </summary>
public class AudioManager : MonoBehaviour
{
    private AudioSource audioData;
    [SerializeField]
    private AudioClip dealSound;
    [SerializeField]
    private AudioClip winSound;
    public static AudioManager Singleton { get; private set; }

    void Awake()
    {
        if (Singleton != null && Singleton != this) Destroy(this);
        else Singleton = this;
    }

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }
    
    /// <summary>
    /// Plays the card dealing sound effect
    /// </summary>
    public void PlayDealSound(){
        audioData.clip = dealSound;
        audioData.Play();
    }

    /// <summary>
    /// Plays the winning jingle
    /// </summary>
    public void PlayWinSound(){
        audioData.clip = winSound;
        audioData.Play();
    }
}