using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseButton : MonoBehaviour
{
    public Button ReturnButton;
    public GameObject StarCanvs;
    private void Awake()
    {
        ReturnButton.onClick.AddListener(OnReturnButton);

    }
    private void OnReturnButton()
    {
        StarCanvs.SetActive(true);
        gameObject.SetActive(false);
    }
}
