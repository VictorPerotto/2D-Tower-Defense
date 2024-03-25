using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraHandler : MonoBehaviour{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomAmount;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    
    private float orthographicSize;
    private float targetOrthographicSize;

    private void Start(){
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    private void Update(){
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement(){
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(x, y).normalized;
        
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void HandleZoom(){
        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;

        float minOrthographicSize = 10f;
        float maxOrthographicSize = 30f;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);
        
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }   
}
