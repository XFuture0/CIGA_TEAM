using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("ÒôÆµµ÷½ÚÆ÷")]
    public AudioMixer MainAudio;
    [Header("ÒôÆµ²¥·ÅÆ÷")]
    public AudioSource MainAudioSource;
    [Header("ÊÂ¼þ¼àÌý")]
    public AudioEventSO AudioClipEvent;
    private void OnEnable()
    {
        AudioClipEvent.OnAudioEventRaised += SetAudioClip;
    }
    private void SetAudioClip(AudioClip thisClip)
    {
        MainAudioSource.clip = thisClip;
        MainAudioSource.Play();
    }

    private void OnDisable()
    {
        AudioClipEvent.OnAudioEventRaised -= SetAudioClip;
    }
    public void SetMainAudioVolume()
    {
        var NewVolume = GameManager.Instance.PlayerData.AudioVolume * 100 - 80;
        MainAudio.SetFloat("MasterVolume", NewVolume);
    }
    
}
