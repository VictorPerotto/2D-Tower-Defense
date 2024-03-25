using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    private BuildingTypeListSO buildingTypeList;
    private Camera mainCamera;

    private void Awake(){
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);  
        //carrega de dentro da pasta nomeada como "Resources" um item da classe "BuildingTypeListSO" que tem o nome de "BuildingTypeListSO"
        buildingType = buildingTypeList.list[0];
    }

    private void Start(){
        mainCamera = Camera.main;  
    }

    private void Update(){
        if(Input.GetMouseButtonDown(0)){
            Instantiate(buildingType.prefab, GetMouseWorldPosition(), quaternion.identity);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            buildingType = buildingTypeList.list[0];
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)){
            buildingType = buildingTypeList.list[1];
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)){
            buildingType = buildingTypeList.list[2];
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
