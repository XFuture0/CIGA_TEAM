using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    public PolygonCollider2D PolygonCollider2D;
    [Header("�㲥")]
    public BoundEventSO BoundEvent;
    private void OnEnable()
    {
        BoundEvent.RaiseEvent(PolygonCollider2D);
    }
}
