using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBox : MonoBehaviour
{
    public GameObject Door;
    private void OnEnable()
    {
        Door.SetActive(false);
        StartCoroutine(OnStartGame());
    }
    private IEnumerator OnStartGame()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).transform.position = transform.GetChild(i).GetComponent<VisitorAI>().BasePosition;
        }
        yield return new WaitForSeconds(0.5f);
        Door.SetActive(true);
    }
}
