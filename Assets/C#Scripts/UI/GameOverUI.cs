using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private int Star=0;//星星数量
    public GameObject Fail;
    private Animator anim;//动画
    public StarUI starUI1;//获取一号星星
    public StarUI starUI2;//获取二号星星
    public StarUI starUI3;//获取三号星星

    private void Awake()
    {
        anim = GetComponent<Animator>();//获取动画
    }
   
    public void Show(int Star)//星星数值
    {
        anim.SetTrigger("IsShow");
        this.Star = Star;
    }
    public void ShowStar()
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
}
