using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    public GameObject Player;
    public PlayerData PlayerData;
    public FadeCanvs FadeCanvs;
    public void FadeIn()
    {
        FadeCanvs.FadeIn();
    }
    public void FadeOut()
    {
        FadeCanvs.FadeOut();
    }
    public void RestartLevel()
    {

    }
    public void LevelList()
    {

    }
}
