using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New SceneData", menuName = "Data/SceneData")]
public class SceneData : ScriptableObject
{
    public string SceneName;
    public float Size;//摄影机画面大小
    public Vector3 ToPosition;//传送位置
}
