using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauntedExhibit : MonoBehaviour
{
    [Header("惊吓设置")]
    public float scareIntensity = 2f; // 惊吓强度
    public float scareDuration = 1f;// 惊吓时间
    [Header("视觉效果")]
    public Color possessionColor = new Color(0.8f, 0.2f, 0.8f, 1f); // 被附身时的颜色
    public Color scareColor = Color.yellow; // 惊吓时的颜色

    private SpriteRenderer exhibitRenderer;
    private Color originalColor;
    public bool isScaring = false;
    public bool isPossessed = false; // 是否被幽灵附身
    private List<VisitorAI> visitorsInRange = new List<VisitorAI>();

    void Start()
    {
        exhibitRenderer = GetComponent<SpriteRenderer>();
        originalColor = exhibitRenderer.color;
    }

    void Update()
    {
        // 只有被附身时才能惊吓
        if (isPossessed && !isScaring && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ScareCoroutine());
        }
    }

    // 外部调用激活惊吓
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
        // 创建计时器
        float timer = 0f;

        while (timer < scareDuration)
        {
            // 每帧更新计时器
            timer += Time.deltaTime;

            // 惊吓范围内的游客
            foreach (VisitorAI visitor in visitorsInRange)
            {
                if (visitor != null)
                {
                    // 基于时间增量施加恐惧值
                    visitor.Scare(scareIntensity * Time.deltaTime);
                }
            }

            yield return null; // 等待下一帧
        }

        // 结束惊吓
        isScaring = false;

        // 恢复颜色：如果仍然被附身，则显示附身颜色，否则恢复原色
        if (isPossessed)
        {
            exhibitRenderer.color = possessionColor;
        }
        else
        {
            exhibitRenderer.color = originalColor;
        }
    }

    // 设置附身状态
    public void SetPossessed(bool possessed)
    {
        isPossessed = possessed;

        if (isPossessed)
        {
            exhibitRenderer.color = possessionColor;
        }
        else
        {
            // 解除附身时立即停止任何惊吓
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
