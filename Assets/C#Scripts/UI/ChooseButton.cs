using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseButton : MonoBehaviour
{
    public Button ReturnButton;
    public GameObject StarCanvs;
    public AudioClip Click;
    private void Awake()
    {
        ReturnButton.onClick.AddListener(OnReturnButton);

    }
    private void OnReturnButton()
    {
        AudioManager.Instance.SetAudioClip(Click, "FX");
        StarCanvs.SetActive(true);
        gameObject.SetActive(false);
    }
}
