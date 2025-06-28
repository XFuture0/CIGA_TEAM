using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvs : MonoBehaviour
{
    public GameObject ChooseCanvs;
    public Button StartButton;
    public Button SettingButton;
    public Button QuitButton;
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
        gameObject.SetActive(false);
        ChooseCanvs.SetActive(true);
    }
    private void OnSettingButton()
    {

    }
    private void OnQuitButton()
    {

    }
}
