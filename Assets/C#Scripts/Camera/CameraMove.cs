using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("��Ե��������")]
    public float edgeScrollSpeed = 25f;
    public float edgeThreshold = 0.05f; // ��Ļ��Ե�����ֵ���ٷֱȣ�
    public float minX;          // �����СXλ��
    public float maxX;           // ������Xλ��
    public float minY;          // �����СYλ��
    public float maxY;           // ������Yλ��
    [Header("��������")]
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

    }

    void Update()
    {

    }

    void LateUpdate()
    {
        EdgeScrollMovement();

        // ����Y�᲻��
        ClampCameraPosition();
    }

    void EdgeScrollMovement()
    {
        Vector2 mouseViewportPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        Vector3 moveDirection = Vector3.zero;

        // ������Ե
        if (mouseViewportPos.x < edgeThreshold)
        {
            moveDirection.x = -1;
        }
        // ����ұ�Ե
        else if (mouseViewportPos.x > 1 - edgeThreshold)
        {
            moveDirection.x = 1;
        }

        // ����±�Ե
        if (mouseViewportPos.y < edgeThreshold)
        {
            moveDirection.y = -1;
        }
        // ����ϱ�Ե
        else if (mouseViewportPos.y > 1 - edgeThreshold)
        {
            moveDirection.y = 1;
        }

        if (moveDirection != Vector3.zero)
        {
            Vector3 newPosition = transform.position + moveDirection * edgeScrollSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }

    void ClampCameraPosition()
    {
        // ͬʱ����X��Y��
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}


