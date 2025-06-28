using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoremanager : SingleTon<Scoremanager>
{
    public ScoreCanvs  scoreCanvs;
    public TimeUI timeUI;
    public void SetScore(float score)
    {
        scoreCanvs.Score += score;
    }
    public void RefreshScore(int Level)
    {
        foreach (var item in GameManager.Instance.PlayerData.StarDatas)
        {
            if(item.Level == Level)
            {
                scoreCanvs.NeedScore = item.Score;
                timeUI.time = item.Second;
            }
        }
    }
}
