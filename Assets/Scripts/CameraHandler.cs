using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float ortographicSize;
    private float targetOrtographicSize;

    private void Start()
    {
        ortographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrtographicSize = ortographicSize;
    }
    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(x, y).normalized;
        float moveSpeed = 30f;

        transform.position += (Vector3)moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        float zoomAmount = 2f;
        targetOrtographicSize += -Input.mouseScrollDelta.y * zoomAmount;

        float minOrtographicSize = 10;
        float maxOrtographicSize = 20;
        targetOrtographicSize = Mathf.Clamp(targetOrtographicSize, minOrtographicSize, maxOrtographicSize);

        float zoomSpeed = 5f;
        ortographicSize = Mathf.Lerp(ortographicSize, targetOrtographicSize, Time.deltaTime * zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = ortographicSize;
    }
}