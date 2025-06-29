using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingleTon<AudioManager>
{
    [Header("��Ƶ������")]
    public AudioMixer MainAudio;
    [Header("��Ƶ������")]
    public AudioSource MainAudioSource;
    public AudioSource FXAudioSource;
    public AudioSource PlayerSource;
    public AudioSource FXSource;
    [Header("�¼�����")]
    public AudioEventSO AudioClipEvent;
    private void OnEnable()
    {
        AudioClipEvent.OnAudioEventRaised += SetAudioClip;
    }
    public void SetAudioClip(AudioClip thisClip, string Name)
    {
        switch (Name)
        {
            case "Player":
                PlayerSource.clip = thisClip;
                PlayerSource.Play();
                break;
            case "NPC":
                FXAudioSource.clip = thisClip;
                FXAudioSource.Play();
                break;
            case "FX":
                FXSource.clip = thisClip;
                FXSource.Play();
                break;
        }
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
