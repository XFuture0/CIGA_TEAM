using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauntedExhibit : MonoBehaviour
{
    [Header("��������")]
    public float scareIntensity = 2f; // ����ǿ��
    public float scareDuration = 1f;// ����ʱ��
    [Header("�Ӿ�Ч��")]
    public Color possessionColor = new Color(0.8f, 0.2f, 0.8f, 1f); // ������ʱ����ɫ
    public Color scareColor = Color.yellow; // ����ʱ����ɫ

    private SpriteRenderer exhibitRenderer;
    private Color originalColor;
    public bool isScaring = false;
    public bool isPossessed = false; // �Ƿ����鸽��
    private List<VisitorAI> visitorsInRange = new List<VisitorAI>();

    void Start()
    {
        exhibitRenderer = GetComponent<SpriteRenderer>();
        originalColor = exhibitRenderer.color;
    }

    void Update()
    {
        // ֻ�б�����ʱ���ܾ���
        if (isPossessed && !isScaring && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ScareCoroutine());
        }
    }

    // �ⲿ���ü����
    public void ActivateScare()
    {
        if (isPossessed && !isScaring)
        {
            StartCoroutine(ScareCoroutine());
        }
    }

    IEnumerator ScareCoroutine()
    {
        isScaring = true;
        // ������ʱ��
        float timer = 0f;

        while (timer < scareDuration)
        {
            // ÿ֡���¼�ʱ��
            timer += Time.deltaTime;

            // ���ŷ�Χ�ڵ��ο�
            foreach (VisitorAI visitor in visitorsInRange)
            {
                if (visitor != null)
                {
                    // ����ʱ������ʩ�ӿ־�ֵ
                    visitor.Scare(scareIntensity * Time.deltaTime);
                }
            }

            yield return null; // �ȴ���һ֡
        }

        // ��������
        isScaring = false;

        // �ָ���ɫ�������Ȼ����������ʾ������ɫ������ָ�ԭɫ
        if (isPossessed)
        {
            exhibitRenderer.color = possessionColor;
        }
        else
        {
            exhibitRenderer.color = originalColor;
        }
    }

    // ���ø���״̬
    public void SetPossessed(bool possessed)
    {
        isPossessed = possessed;

        if (isPossessed)
        {
            exhibitRenderer.color = possessionColor;
        }
        else
        {
            // �������ʱ����ֹͣ�κξ���
            isScaring = false;
            StopAllCoroutines();
            exhibitRenderer.color = originalColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("visitor"))
        {
            VisitorAI visitor = other.GetComponent<VisitorAI>();
            if (visitor != null && !visitorsInRange.Contains(visitor))
            {
                visitorsInRange.Add(visitor);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("visitor"))
        {
            VisitorAI visitor = other.GetComponent<VisitorAI>();
            if (visitor != null && visitorsInRange.Contains(visitor))
            {
                visitorsInRange.Remove(visitor);
            }
        }
    }


}
