using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] Transform woodHarvester;
    private Camera mainCamera;

    private void Start(){
        mainCamera = Camera.main;    
    }

    private void Update(){
        if(Input.GetMouseButtonDown(0)){
            Instantiate(woodHarvester, GetMouseWorldPosition(), quaternion.identity);
        }
    }

    private Vector3 GetMouseWorldPosition(){
        /*
            Input.mousePosition - retorna a posição do mouse em pixels da tela
            ScreenToWorldPoint - transforma uma posição em pixels da camera para uma posição em relação ao mundo 
        */
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
