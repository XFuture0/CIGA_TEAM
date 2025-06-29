using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardLight : MonoBehaviour
{
    public VoidEventSO GameOverEvent;
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && other.GetComponent<GhostController>().IsPossessing())
        {
            GameOverEvent.RaiseEvent();
        }
    }
}
