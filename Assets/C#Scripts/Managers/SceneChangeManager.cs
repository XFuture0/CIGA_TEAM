using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : SingleTon<SceneChangeManager>
{
    public void ChangeScene(SceneData NextScene)
    {
        StartCoroutine(OnChangeScene(NextScene));
    }
    private IEnumerator OnChangeScene(SceneData NextScene)
    {
        //    SceneManager.UnloadSceneAsync(CurrentScene.SceneName);
        GameManager.Instance.FadeIn();
        Scoremanager.Instance.scoreCanvs.Score = 0;
        Scoremanager.Instance.RefreshScore((int)NextScene.Size);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(NextScene.SceneName, LoadSceneMode.Additive);
        DataManager.Instance.Save(DataManager.Instance.Index);//´æµµ
        GameManager.Instance.FadeOut();
        yield return null;
    }
}
