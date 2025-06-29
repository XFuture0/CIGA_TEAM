using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    private Animator anim;//动画
    public GameObject ChooseCanvs;
    public GameObject PasePanel;
    void Start()
    {
        anim = GetComponent<Animator>();//获取动画
    }
    public void OnPauseButtonClick()//停止按钮
    {
        PasePanel.SetActive(true);
        Time.timeScale = 0;//时间为0，时间停止
    }
    public void OnContinueButtonClick()//继续按钮
    {
        Time.timeScale = 1;//时间为1，时间继续
        PasePanel.SetActive(false);
    }
    public void OnLevelListButtonClick()//关卡按钮
    {
        Time.timeScale = 1;//时间为1，时间继续
        ChooseCanvs.SetActive(true);
        PasePanel.SetActive(false);
        SceneChangeManager.Instance.CloseScene(GameManager.Instance.PlayerData.SceneData);
    }
}
