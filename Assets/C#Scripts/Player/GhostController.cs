using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float moveSpeed = 5f;
    public float minDistanceToTarget = 0.1f;
    private bool IsSet;
    [Header("��������")]
    public Color possessionColor = new Color(0.8f, 0.2f, 0.8f, 1f); // ����ʱ����ɫ

    private bool isMoving = false;
    private HauntedExhibit possessedExhibit; // ��ǰ�����չƷ
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
        // ������ƶ�
        if (Input.GetMouseButtonDown(0) && !IsPossessing())
        {
            HandleMouseClick();
        }

        // �ƶ��߼�
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
        // ����״̬�¿ո������
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

        // �˳�����״̬
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
        // �����λ��ת��Ϊ��������
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z; // ����Z��һ��

        // ��������Ƿ���չƷ
        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, Vector2.zero);
        foreach (var item in hit)
        {
            if (item.collider != null && item.collider.CompareTag("Exhibit"))
            {
                // ���Ը���չƷ
                AttemptPossession(item.collider.gameObject);
                return;
            }
        }
        // �ƶ������λ��
        SetMoveTarget(mousePos);
    }

    void AttemptPossession(GameObject exhibit)
    {
        // ��ȡչƷ��HauntedExhibit���
        HauntedExhibit hauntedExhibit = exhibit.GetComponent<HauntedExhibit>();
        if (hauntedExhibit != null)
        {
            // ����չƷ
            PossessExhibit(hauntedExhibit);
        }
    }

    void PossessExhibit(HauntedExhibit exhibit)
    {
        // ���ø���״̬
        possessedExhibit = exhibit;
        possessedExhibit.SetPossessed(true);

        // �����ƶ���չƷλ��
        SetMoveTarget(possessedExhibit.transform.position);

        // �����ɫ��ʾ����״̬
    //    ghostRenderer.color = possessionColor;

        Debug.Log("���鸽��: " + possessedExhibit.name);
    }

    void ReleasePossession()
    {
        if (!IsPossessing()) return;

        // �������״̬
        possessedExhibit.SetPossessed(false);
        possessedExhibit = null;

        // �ָ�������ɫ
        ghostRenderer.color = Color.white;
        ghostRenderer.enabled = true;
        IsSet = false;
        Debug.Log("����������");
    }

    void SetMoveTarget(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    void MoveToTarget()
    {
        // �����ƶ�����
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // �ƶ�����
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // ����Ƿ񵽴�Ŀ��λ��
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance <= minDistanceToTarget)
        {
            isMoving = false;

            // ���������Ǹ���չƷ����������
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
