using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.ExceptionServices;
using UnityEngine.UI;


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
    public void PlayDealSound(){
        audioData.clip = dealSound;
        audioData.Play();
    }

    public void PlayWinSound(){
        audioData.clip = winSound;
        audioData.Play();
    }
}