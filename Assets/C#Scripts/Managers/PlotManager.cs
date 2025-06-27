using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : SingleTon<PlotManager>
{
    public PlotData PlotData;
    public string SetPlot(string SpeakerName,int Level)//对话的使用要输入说话者的名字和说话的台词序号(没有要在PlotData中添加)
    {
        foreach (var ThisPolt in PlotData.PlotExcerpts)
        {
            if(ThisPolt.Name == SpeakerName)
            {
                return ThisPolt.Excerpts[Level].Text;
            }
        }
        return null;
    }
}
