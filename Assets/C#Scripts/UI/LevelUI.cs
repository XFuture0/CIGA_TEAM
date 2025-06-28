using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    public GameObject unlockGo;
    public GameObject lockGo;

    public TextMeshProUGUI levelNumberText;
    public GameObject star0Go;
    public GameObject star1Go;
    public GameObject star2Go;
    public GameObject star3Go;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Show(int Star)
    {
        star0Go.SetActive(false);
        star1Go.SetActive(false);
        star2Go.SetActive(false);
        star3Go.SetActive(false);
        if (Star < 0)
        {
            unlockGo.SetActive(false);
            lockGo.SetActive(true);
        }
        else
        {
            unlockGo.SetActive(true);
            lockGo.SetActive(false);
            if (Star == 3)
            {
                star3Go.SetActive(true);
            }
            else if (Star == 2)
            {
                star2Go.SetActive(true);
            }
            else if (Star == 1)
            {
                star1Go.SetActive(true);
            }
            else if (Star == 0)
            {
                star0Go.SetActive(true);
            }
        }
    }
    public void OnClick1()
    {
        SceneManager.LoadScene(2);
    }
    public void OnClick2()
    {
        SceneManager.LoadScene(3);
    }
    public void OnClick3()
    {
       SceneManager.LoadScene(4);
    }
}
