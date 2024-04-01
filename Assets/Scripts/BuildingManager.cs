using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance {get; private set;}

    private BuildingTypeSO activeBuildingType;
    private BuildingTypeListSO buildingTypeList;
    private Camera mainCamera;

    private void Awake(){
        Instance = this;

        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);  
        //carrega de dentro da pasta nomeada como "Resources" um item da classe "BuildingTypeListSO" que tem o nome de "BuildingTypeListSO"
    }

    private void Start(){
        mainCamera = Camera.main;  
    }

    private void Update(){
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()){
            if(activeBuildingType != null){
                Instantiate(activeBuildingType.prefab, GetMouseWorldPosition(), quaternion.identity);
            }
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

    public void SetActiveBuildingType(BuildingTypeSO buildingType){
        activeBuildingType = buildingType;
    }

    public BuildingTypeSO GetActiveBuildingType(){
        return activeBuildingType;
    }
}
