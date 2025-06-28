using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/BoundEventSO")]

public class BoundEventSO : ScriptableObject
{
    public UnityAction<PolygonCollider2D> OnPolygonColliderEventRaised;
    public void RaiseEvent(PolygonCollider2D polygonCollider2D)
    {
        OnPolygonColliderEventRaised?.Invoke(polygonCollider2D);
    }

}
