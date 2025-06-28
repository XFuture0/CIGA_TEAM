using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvs : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void FadeIn()
    {
        anim.SetTrigger("FadeIn");
    }
    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }
}
