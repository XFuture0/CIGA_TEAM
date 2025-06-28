using Cinemachine;
using System;
using UnityEngine;
public class CameraBound : MonoBehaviour
{
    public CinemachineConfiner2D Confiner;
    [Header("ÊÂ¼þ¼àÌý")]
    public BoundEventSO BoundEvent;
    private void OnEnable()
    {
        BoundEvent.OnPolygonColliderEventRaised += SetBound;
    }

    private void SetBound(PolygonCollider2D bound)
    {
        Confiner.m_BoundingShape2D = bound;
    }

    private void OnDisable()
    {
        BoundEvent.OnPolygonColliderEventRaised -= SetBound;
    }
}
