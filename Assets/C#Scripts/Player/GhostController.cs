using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 5f;
    public float minDistanceToTarget = 0.1f;
    private bool IsSet;
    [Header("附身设置")]
    public Color possessionColor = new Color(0.8f, 0.2f, 0.8f, 1f); // 附身时的颜色

    private bool isMoving = false;
    private HauntedExhibit possessedExhibit; // 当前附身的展品
    private SpriteRenderer ghostRenderer;
    private Color originalColor;
    private Vector3 targetPosition;
    public LayerMask Exhibit;
    public AudioEventSO AudioEvent;
    public AudioClip audioClip;
    void Awake()
    {
        ghostRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        ghostRenderer.enabled = true;
        ghostRenderer.color = Color.white;
    }

    void Update()
    {
        // 鼠标点击移动
        if (Input.GetMouseButtonDown(0) && !IsPossessing())
        {
            HandleMouseClick();
        }

        // 移动逻辑
        if (isMoving)
        {
            MoveToTarget();
        }
        if (possessedExhibit != null && Physics2D.OverlapCircle(transform.position, 0.1f, Exhibit))
        {
            if (Physics2D.OverlapCircle(transform.position, 0.1f, Exhibit).gameObject == possessedExhibit.gameObject && !IsSet)
            {
                IsSet = true;
                AudioEvent.RaiseEvent(audioClip,"Player");
            }
        }
        // 附身状态下空格键惊吓
        if (IsPossessing() && Input.GetKeyDown(KeyCode.Space))
        {
            if(possessedExhibit != null && Physics2D.OverlapCircle(transform.position, 0.1f, Exhibit))
            {
                if (Physics2D.OverlapCircle(transform.position, 0.1f, Exhibit).gameObject == possessedExhibit.gameObject)
                {
                    possessedExhibit.ActivateScare();
                }
            }
        }

        // 退出附身状态
        if (IsPossessing() && Input.GetKeyDown(KeyCode.E))
        {
            if(possessedExhibit != null)
            {
                if (!possessedExhibit.isScaring)
                {
                    ReleasePossession();
                }
            }
        }
    }

    void HandleMouseClick()
    {
        // 将鼠标位置转换为世界坐标
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z; // 保持Z轴一致

        // 检测点击的是否是展品
        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, Vector2.zero);
        foreach (var item in hit)
        {
            if (item.collider != null && item.collider.CompareTag("Exhibit"))
            {
                // 尝试附身展品
                AttemptPossession(item.collider.gameObject);
                return;
            }
        }
        // 移动到点击位置
        SetMoveTarget(mousePos);
    }

    void AttemptPossession(GameObject exhibit)
    {
        // 获取展品的HauntedExhibit组件
        HauntedExhibit hauntedExhibit = exhibit.GetComponent<HauntedExhibit>();
        if (hauntedExhibit != null)
        {
            // 附身展品
            PossessExhibit(hauntedExhibit);
        }
    }

    void PossessExhibit(HauntedExhibit exhibit)
    {
        // 设置附身状态
        possessedExhibit = exhibit;
        possessedExhibit.SetPossessed(true);

        // 幽灵移动到展品位置
        SetMoveTarget(possessedExhibit.transform.position);

        // 幽灵变色表示附身状态
    //    ghostRenderer.color = possessionColor;

        Debug.Log("幽灵附身到: " + possessedExhibit.name);
    }

    void ReleasePossession()
    {
        if (!IsPossessing()) return;

        // 解除附身状态
        possessedExhibit.SetPossessed(false);
        possessedExhibit = null;

        // 恢复幽灵颜色
        ghostRenderer.color = Color.white;
        ghostRenderer.enabled = true;
        IsSet = false;
        Debug.Log("幽灵解除附身");
    }

    void SetMoveTarget(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    void MoveToTarget()
    {
        // 计算移动方向
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // 移动幽灵
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 检查是否到达目标位置
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance <= minDistanceToTarget)
        {
            isMoving = false;

            // 如果到达的是附身展品，隐藏幽灵
            if (IsPossessing() && distance <= minDistanceToTarget * 2)
            {
                ghostRenderer.enabled = false;
            }
        }
    }

    public bool IsPossessing()
    {
        return possessedExhibit != null;
    }
}
