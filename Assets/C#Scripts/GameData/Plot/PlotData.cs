using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Plot List", menuName = "List/Plot List")]
public class PlotData : ScriptableObject
{
    [System.Serializable]
    public class ExcerptText
    {
        [TextArea]
        public string Text;//�Ի�����
    }
    [System.Serializable]
    public class PlotExcerpt
    {
        public string Name;//�Ի�����
        public List<ExcerptText> Excerpts;//�Ի�Ƭ��
    }
    public List<PlotExcerpt> PlotExcerpts = new List<PlotExcerpt>();
}
