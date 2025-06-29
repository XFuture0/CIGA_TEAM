using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    [System.Serializable]
    public class StarData
    {
        public int Second;//����
        public int Star;//������
        public int Score;//����
        public float minX;          // �����СXλ��
        public float maxX;           // ������Xλ��
        public float minY;          // �����СYλ��
        public float maxY;           // ������Yλ��
        public int Level;//���
    }
    public SceneData SceneData;
    public Vector3 PlayerPosition;//���λ��
    public List<StarData> StarDatas = new List<StarData>();//��¼ÿ�ص�������
    public int RoomIndex;//��ǰͨ���ؿ���
    public float AudioVolume;//��Ϸ����
}
