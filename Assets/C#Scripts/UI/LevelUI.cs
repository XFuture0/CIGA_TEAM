using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public int LevelNumber;
    public Button ChooseButton;
    public SceneData SceneTOGO;
    public GameObject unlockGo;
    public GameObject lockGo;

    public TextMeshProUGUI levelNumberText;
    public GameObject star0Go;
    public GameObject star1Go;
    public GameObject star2Go;
    public GameObject star3Go;
    private void Awake()
    {
        ChooseButton.onClick.AddListener(ChooseLevel);
    }
    private void ChooseLevel()
    {
        if (!lockGo.activeSelf)
        {
            SceneChangeManager.Instance.ChangeScene(SceneTOGO);
            gameObject.transform.parent.parent.gameObject.SetActive(false);
        }
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
}
