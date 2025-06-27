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
        public string Text;//对话内容
    }
    [System.Serializable]
    public class PlotExcerpt
    {
        public string Name;//对话名称
        public List<ExcerptText> Excerpts;//对话片段
    }
    public List<PlotExcerpt> PlotExcerpts = new List<PlotExcerpt>();
}
