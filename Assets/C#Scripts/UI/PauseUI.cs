using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    private Animator anim;//动画
    void Start()
    {
        anim = GetComponent<Animator>();//获取动画
    }
    public void OnPauseButtonClick()//停止按钮
    {
        Time.timeScale = 0;//时间为0，时间停止
        anim.SetBool("IsShow", true);
    }
    public void OnContinueButtonClick()//继续按钮
    {
        Time.timeScale = 1;//时间为1，时间继续
        anim.SetBool("IsShow", false);
    }
    public void OnRestartButtonClick()//重新开始按钮
    {

    }
    public void OnLevelListButtonClick()//关卡按钮
    {

    }
}
