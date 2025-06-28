using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public GameObject ChooseCanvs;
    public GameObject Fail;
    public StarUI starUI1;//获取一号星星
    public StarUI starUI2;//获取二号星星
    public StarUI starUI3;//获取三号星星
    public Button ReturnButton;
    private void Awake()
    {
        ReturnButton.onClick.AddListener(OnReturnButton);
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        Show(Scoremanager.Instance.scoreCanvs.StarCount);
    }
    public void Show(int Star)//星星数值
    {
        Time.timeScale = 0;
        ShowStar(Star);
    }
    public void ShowStar(int Star)
    {
        if (Star == 0)
        {
            Fail.SetActive(true);
        }
        if (Star >= 1)
        {
            starUI1.Show();
        }
        if (Star >= 2)
        {
            starUI2.Show();
        }
        if (Star >= 3)
        {
            starUI3.Show();
        }
    }
    public  void OnReturnButton()
    {
        Time.timeScale = 1;//时间为1，时间继续
        ChooseCanvs.SetActive(true);
        gameObject.SetActive(false);
        SceneChangeManager.Instance.CloseScene(GameManager.Instance.PlayerData.SceneData);
    }

}
