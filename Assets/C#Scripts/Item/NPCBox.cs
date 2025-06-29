using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBox : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(OnStartGame());
    }
    private IEnumerator OnStartGame()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            if (transform.GetChild(i).gameObject.tag == "visitor")
            {
                transform.GetChild(i).transform.position = transform.GetChild(i).GetComponent<VisitorAI>().BasePosition;
            }
            else if (transform.GetChild(i).gameObject.tag == "Guard")
            {
                transform.GetChild(i).transform.position = transform.GetChild(i).GetComponent<Guard>().BasePosition;
            }
        }
        yield return new WaitForSeconds(0.5f);
    }
}
