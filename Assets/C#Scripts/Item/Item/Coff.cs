using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coff : MonoBehaviour
{
    private Animator anim;
    [Header("�¼�����")]
    public VoidEventSO UseEvent;
    [Header("���ż�ʱ��")]
    private float ScareTime;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (ScareTime > -2)
        {
            ScareTime -= Time.deltaTime;
        }
        if (ScareTime >= 0)
        {
            anim.SetBool("Use", gameObject.GetComponent<HauntedExhibit>().isScaring);
        }
        if (ScareTime < 0)
        {
            anim.SetBool("Use", false);
        }
    }
    private void OnEnable()
    {
        UseEvent.OnEventRaised += OnUse;
    }
    private void OnDisable()
    {
        UseEvent.OnEventRaised -= OnUse;
    }
    private void OnUse()
    {
        if (gameObject.GetComponent<HauntedExhibit>().isPossessed)
        {
            ScareTime = gameObject.GetComponent<HauntedExhibit>().scareDuration;
        }
    }
}
