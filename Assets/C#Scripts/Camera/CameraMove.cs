using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("边缘滚动设置")]
    public float edgeScrollSpeed = 25f;
    public float edgeThreshold = 0.05f; // 屏幕边缘检测阈值（百分比）
    public float minX;          // 相机最小X位置
    public float maxX;           // 相机最大X位置
    public float minY;          // 相机最小Y位置
    public float maxY;           // 相机最大Y位置
    [Header("跟随设置")]
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

        // 保持Y轴不变
        ClampCameraPosition();
    }

    void EdgeScrollMovement()
    {
        Vector2 mouseViewportPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        Vector3 moveDirection = Vector3.zero;

        // 检测左边缘
        if (mouseViewportPos.x < edgeThreshold)
        {
            moveDirection.x = -1;
        }
        // 检测右边缘
        else if (mouseViewportPos.x > 1 - edgeThreshold)
        {
            moveDirection.x = 1;
        }

        // 检测下边缘
        if (mouseViewportPos.y < edgeThreshold)
        {
            moveDirection.y = -1;
        }
        // 检测上边缘
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
        // 同时限制X和Y轴
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}


