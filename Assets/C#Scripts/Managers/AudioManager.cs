using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("��Ƶ������")]
    public AudioMixer MainAudio;
    [Header("��Ƶ������")]
    public AudioSource MainAudioSource;
    [Header("�¼�����")]
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
