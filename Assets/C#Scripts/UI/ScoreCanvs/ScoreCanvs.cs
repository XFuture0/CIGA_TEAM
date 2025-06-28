using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCanvs : MonoBehaviour
{
    public GameObject Star;
    public int StarCount;
    public float Score;
    public float NeedScore;
    [Header("ÐÇÊý")]
    public Sprite Star0;
    public Sprite Star1;
    public Sprite Star2;
    public Sprite Star3;
    private void Update()
    {
        SetStar();
    }
    public void SetStar()
    {
        if(Score < NeedScore * 0.33f)
        {
            StarCount = 0;
            Star.GetComponent<Image>().sprite = Star0;
        }
        if(Score > NeedScore * 0.33f && Score < NeedScore * 0.66f)
        {
            StarCount = 1;
            Star.GetComponent<Image>().sprite = Star1;
        }
        if(Score > NeedScore * 0.66f && Score < NeedScore)
        {
            StarCount = 2;
            Star.GetComponent<Image>().sprite = Star2;
        }
        if(Score == NeedScore)
        {
            StarCount = 3;
            Star.GetComponent<Image>().sprite = Star3;
        }
    }
}
