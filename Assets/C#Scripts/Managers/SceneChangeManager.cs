using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : SingleTon<SceneChangeManager>
{
    public CameraMove CameraMove;
    public GameObject TimeCanvs;
    public TimeUI TimeUI;
    public void ChangeScene(SceneData NextScene)
    {
        StartCoroutine(OnChangeScene(NextScene));
    }
    private IEnumerator OnChangeScene(SceneData NextScene)
    {
        GameManager.Instance.FadeIn();
        Scoremanager.Instance.scoreCanvs.Score = 0;
        Scoremanager.Instance.RefreshScore((int)NextScene.Size);
        var level = (int)NextScene.Size;
        CameraMove.RefreshCameraPo(GameManager.Instance.PlayerData.StarDatas[level].minX, GameManager.Instance.PlayerData.StarDatas[level].minY, GameManager.Instance.PlayerData.StarDatas[level].maxX, GameManager.Instance.PlayerData.StarDatas[level].maxY);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(NextScene.SceneName, LoadSceneMode.Additive);
        GameManager.Instance.Player.SetActive(true);
        GameManager.Instance.Player.transform.position = new Vector3(-7, -3,0);
        GameManager.Instance.PlayerData.SceneData = NextScene;
        DataManager.Instance.Save(DataManager.Instance.Index);//´æµµ
        TimeUI.IsStop = false;
        TimeCanvs.SetActive(true);
        TimeUI.time = GameManager.Instance.PlayerData.StarDatas[level].Second;
        GameManager.Instance.FadeOut();
        yield return null;
    }
    public void CloseScene(SceneData NextScene)
    {
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
        AudioManager.Instance.SetAudioClip(null, "NPC");
        TimeCanvs.SetActive(false);
        GameManager.Instance.FadeOut();

    }
}
