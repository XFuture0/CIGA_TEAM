using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elev : MonoBehaviour
{
    public Vector3 ToGoElev;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "visitor")
        {
            if (other.GetComponent<VisitorAI>().CanElev)
            {
                other.gameObject.GetComponent<VisitorAI>().ElevTimer = 5f;
                other.gameObject.transform.position = ToGoElev;
                other.gameObject.GetComponent<VisitorAI>().ChangeSpeed();
            }
        }
        if(other.tag == "Guard")
        {
            if (other.GetComponent<Guard>().CanElev)
            {
                other.gameObject.transform.position = ToGoElev;
                other.GetComponent<Guard>().SetElev();
            }
        }
    }
}
