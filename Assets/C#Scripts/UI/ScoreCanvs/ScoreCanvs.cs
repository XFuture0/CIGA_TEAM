using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCanvs : MonoBehaviour
{
    public GameObject Star;
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
            Star.GetComponent<Image>().sprite = Star0;
        }
        if(Score > NeedScore * 0.33f && Score < NeedScore * 0.66f)
        {
            Star.GetComponent<Image>().sprite = Star1;
        }
        if(Score > NeedScore * 0.66f && Score < NeedScore)
        {
            Star.GetComponent<Image>().sprite = Star2;
        }
        if(Score == NeedScore)
        {
            Star.GetComponent<Image>().sprite = Star3;
        }
    }
}
