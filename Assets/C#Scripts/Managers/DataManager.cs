using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : SingleTon<DataManager>
{
    public int Index;
    private BinaryFormatter formatter;
    private FileStream PlayerDataFile;//玩家基本数据文件
    [Header("数据")]
    public PlayerData PlayerData;//玩家基本数据
    protected override void Awake()
    {
        base.Awake();
        formatter = new BinaryFormatter();
    }
    public void Save(int index)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
        }
        //总文件夹
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData" + "/Save" + index);
        }
        //存档文件夹
        var Json = JsonUtility.ToJson(GameManager.Instance.PlayerData);
        JsonUtility.FromJsonOverwrite(Json.ToString(), PlayerData);
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerData.txt").Dispose();
        }
        PlayerDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(PlayerData);
        formatter.Serialize(PlayerDataFile, Json);
        PlayerDataFile.Close();
        //玩家基本数据
    }
    public void Load(int index)
    {
        Index = index;
        if (Directory.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index))
        {
            PlayerDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(PlayerDataFile).ToString(), PlayerData);
            var Json = JsonUtility.ToJson(PlayerData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), GameManager.Instance.PlayerData);
            PlayerDataFile.Close();
            //玩家基本数据
        }
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index))
        {
            
            //恢复数据初始化(判定当前存档为空时使用)
        }
    }
  
    public void Delete(int index)
    {
        if (Directory.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index))
        {
            foreach (var file in Directory.GetFiles(Application.persistentDataPath + "/SaveData" + "/Save" + index))
            {
                File.Delete(file);
            }
            Directory.Delete(Application.persistentDataPath + "/SaveData" + "/Save" + index);
        }
        //清除当前存档
    }
}
