using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : SingleTon<SceneChangeManager>
{
    public GameObject TimeCanvs;
    public void ChangeScene(SceneData NextScene)
    {
        StartCoroutine(OnChangeScene(NextScene));
    }
    private IEnumerator OnChangeScene(SceneData NextScene)
    {
        GameManager.Instance.FadeIn();
        Scoremanager.Instance.scoreCanvs.Score = 0;
        Scoremanager.Instance.RefreshScore((int)NextScene.Size);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(NextScene.SceneName, LoadSceneMode.Additive);
        GameManager.Instance.Player.SetActive(true);
        GameManager.Instance.Player.transform.position = new Vector3(-7, -3,0);
        GameManager.Instance.PlayerData.SceneData = NextScene;
        DataManager.Instance.Save(DataManager.Instance.Index);//´æµµ
        TimeCanvs.SetActive(true);
        GameManager.Instance.FadeOut();
        yield return null;
    }
    public void CloseScene(SceneData NextScene)
    {
        Debug.Log(2);
        StartCoroutine(OnUnloadScene(NextScene));
    }
    private IEnumerator OnUnloadScene(SceneData NextScene)
    {
        GameManager.Instance.FadeIn();
        Scoremanager.Instance.scoreCanvs.Score = 0;
        yield return new WaitForSeconds(0.5f);
        SceneManager.UnloadSceneAsync(NextScene.SceneName);
        GameManager.Instance.Player.SetActive(false);
        GameManager.Instance.Player.transform.position = new Vector3(-7, -3, 0);
        DataManager.Instance.Save(DataManager.Instance.Index);//´æµµ
        TimeCanvs.SetActive(false);
        GameManager.Instance.FadeOut();

    }
}
