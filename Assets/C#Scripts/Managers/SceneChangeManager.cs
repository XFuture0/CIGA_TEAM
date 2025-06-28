using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : SingleTon<SceneChangeManager>
{
    public void ChangeScene(SceneData CurrentScene, SceneData NextScene)
    {
        StartCoroutine(OnChangeScene(CurrentScene, NextScene));
    }
    private IEnumerator OnChangeScene(SceneData CurrentScene, SceneData NextScene)
    {
        SceneManager.UnloadSceneAsync(CurrentScene.SceneName);
        SceneManager.LoadSceneAsync(NextScene.SceneName, LoadSceneMode.Additive);
        DataManager.Instance.Save(DataManager.Instance.Index);//´æµµ
        yield return null;
    }
}
