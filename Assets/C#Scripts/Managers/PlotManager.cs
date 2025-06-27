using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : SingleTon<PlotManager>
{
    public PlotData PlotData;
    public string SetPlot(string SpeakerName,int Level)//�Ի���ʹ��Ҫ����˵���ߵ����ֺ�˵����̨�����(û��Ҫ��PlotData�����)
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
