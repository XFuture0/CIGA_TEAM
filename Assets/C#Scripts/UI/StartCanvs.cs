using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvs : MonoBehaviour
{
    public GameObject ChooseCanvs;
    public GameObject SettingCanvs;
    public Button StartButton;
    public Button SettingButton;
    public Button QuitButton;
    public AudioClip Click;
    private void Awake()
    {
        StartButton.onClick.AddListener(OnStartButton);
        SettingButton.onClick.AddListener(OnSettingButton);
        QuitButton.onClick.AddListener(OnQuitButton);
    }
    public void LevelGame()
    {

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private void OnStartButton()
    {
        AudioManager.Instance.SetAudioClip(Click, "FX");
        gameObject.SetActive(false);
        ChooseCanvs.SetActive(true);
    }
    private void OnSettingButton()
    {
        AudioManager.Instance.SetAudioClip(Click, "FX");
        gameObject.SetActive(false);
        SettingCanvs.SetActive(true);
    }
    private void OnQuitButton()
    {
        AudioManager.Instance.SetAudioClip(Click, "FX");
        Application.Quit();
    }
}
