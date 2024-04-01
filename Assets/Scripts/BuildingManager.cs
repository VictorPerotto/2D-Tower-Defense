using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance {get; private set;}

    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs{
        public BuildingTypeSO activeBuildingType;
    }

    private BuildingTypeSO activeBuildingType;
    private BuildingTypeListSO buildingTypeList;

    private void Awake(){
        Instance = this;

        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);  
        //carrega de dentro da pasta nomeada como "Resources" um item da classe "BuildingTypeListSO" que tem o nome de "BuildingTypeListSO"
    }

    private void Update(){
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()){
            if(activeBuildingType != null){
                Instantiate(activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), quaternion.identity);
            }
        }
    }

    

    public void SetActiveBuildingType(BuildingTypeSO buildingType){
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, 
            new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType }
        );
    }

    public BuildingTypeSO GetActiveBuildingType(){
        return activeBuildingType;
    }
}
