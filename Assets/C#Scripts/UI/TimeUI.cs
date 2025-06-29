using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using System.Threading;

public class TimeUI : MonoBehaviour
{
    public float time;//倒计时
    public Text timeText;
    private bool isTimeOut = false;
    public GameObject gameOverUI;
    public VoidEventSO GameOverevent;
    public AudioClip Victory;
    public AudioClip GameOver;
    public bool IsStop;
    public GameObject Fail;
    void Update()
    {
        if (!IsStop)
        {
            Timer();
        }
        if(Scoremanager.Instance.scoreCanvs.StarCount == 3)
        {
            AudioManager.Instance.SetAudioClip(Victory, "NPC");
            gameOverUI.SetActive(true);
        }
    }
    private void Timer()
    {
        if (isTimeOut == false)
        {
            time -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            
            // 格式化显示
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
          
            if (time <= 0)
            {
                isTimeOut = true;
                timeText.text = "00:00";
                gameOverUI.SetActive(true);
                AudioManager.Instance.SetAudioClip(Victory, "NPC");
            }
        }
    }
    private void OnEnable()
    {
        GameOverevent.OnEventRaised += OnGameOver;
    }

    private void OnGameOver()
    {
        if (!gameOverUI.activeSelf)
        {
            IsStop = true;
            Fail.SetActive(true);
            AudioManager.Instance.SetAudioClip(GameOver, "FX");
            GameManager.Instance.Player.SetActive(false);
            gameOverUI.SetActive(true);
        }
    }

    private void OnDisable()
    {
        GameOverevent.OnEventRaised -= OnGameOver;
    }
}
