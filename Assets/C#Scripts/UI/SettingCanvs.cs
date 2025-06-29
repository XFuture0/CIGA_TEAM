using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingCanvs : MonoBehaviour
{
    public GameObject StartCanvs;
    [SerializeField] private Button returnButton;
    public Slider volumeSlider;
    public AudioClip Click;
    [Header("ÒôÆµµ÷½ÚÆ÷")]
    public AudioMixer MainAudio;

    private void Awake()
    {
        returnButton.onClick.AddListener(ReturnOnclick);
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    private void OnVolumeChange(float volume)
    {
        var NewVolume = volume * 100 - 80;
        MainAudio.SetFloat("MasterVolume", NewVolume);
    }

    public void ReturnOnclick()
    {
        AudioManager.Instance.SetAudioClip(Click, "FX");
        StartCanvs.SetActive(true);
        gameObject.SetActive(false);
    }
}
