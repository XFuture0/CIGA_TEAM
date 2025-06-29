using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    [System.Serializable]
    public class StarData
    {
        public int Second;//秒数
        public int Star;//星星数
        public int Score;//分数
        public float minX;          // 相机最小X位置
        public float maxX;           // 相机最大X位置
        public float minY;          // 相机最小Y位置
        public float maxY;           // 相机最大Y位置
        public int Level;//序号
    }
    public SceneData SceneData;
    public Vector3 PlayerPosition;//玩家位置
    public List<StarData> StarDatas = new List<StarData>();//记录每关的星星数
    public int RoomIndex;//当前通过关卡数
    public float AudioVolume;//游戏音量
}
