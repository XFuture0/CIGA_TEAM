using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float moveSpeed = 5f;
    public float minDistanceToTarget = 0.1f;

    [Header("��������")]
    public Color possessionColor = new Color(0.8f, 0.2f, 0.8f, 1f); // ����ʱ����ɫ

    private Vector3 targetPosition;
    private bool isMoving = false;
    private HauntedExhibit possessedExhibit; // ��ǰ�����չƷ
    private SpriteRenderer ghostRenderer;
    private Color originalColor;
    [Header("�㲥")]
    public VoidEventSO UseEvent;

    void Start()
    {
        ghostRenderer = GetComponent<SpriteRenderer>();
        originalColor = ghostRenderer.color;
        targetPosition = transform.position; // ��ʼλ��
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

        // ����״̬�¿ո������
        if (IsPossessing() && Input.GetKeyDown(KeyCode.Space))
        {
            UseEvent.RaiseEvent();
            possessedExhibit.ActivateScare();
        }

        // �˳�����״̬
        if (IsPossessing() && Input.GetKeyDown(KeyCode.E))
        {
            ReleasePossession();
        }
    }

    void HandleMouseClick()
    {
        // �����λ��ת��Ϊ��������
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z; // ����Z��һ��

        // ��������Ƿ���չƷ
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Exhibit"))
        {
            // ���Ը���չƷ
            AttemptPossession(hit.collider.gameObject);
        }
        else
        {
            // �ƶ������λ��
            SetMoveTarget(mousePos);
        }
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
        ghostRenderer.color = possessionColor;

        Debug.Log("���鸽��: " + possessedExhibit.name);
    }

    void ReleasePossession()
    {
        if (!IsPossessing()) return;

        // �������״̬
        possessedExhibit.SetPossessed(false);
        possessedExhibit = null;

        // �ָ�������ɫ
        ghostRenderer.color = originalColor;
        ghostRenderer.enabled = true;
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
