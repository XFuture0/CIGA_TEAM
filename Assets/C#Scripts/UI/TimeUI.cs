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



    void Update()
    {
        Timer();
        if(Scoremanager.Instance.scoreCanvs.StarCount == 3)
        {
            StartCoroutine("GameOverUI");
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
                StartCoroutine("GameOverUI");
            }
        }
    }
    IEnumerator GameOverUI()
    {
        gameOverUI.SetActive(true);
         yield break; 
    }

}
