using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarUI : MonoBehaviour
{
    private Animator anim;//动画

    private void Awake()
    {
        anim = GetComponent<Animator>();//获取动画
    }
    public void Show()
    {
    anim.SetTrigger("IsShow");
    }
}
