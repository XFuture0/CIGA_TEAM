using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/AudioEventSO")]
public class AudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip,string> OnAudioEventRaised;
    public void RaiseEvent(AudioClip audioClip,string Name)
    {
        OnAudioEventRaised?.Invoke(audioClip,Name);
    }
}
