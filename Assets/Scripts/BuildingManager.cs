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

    [SerializeField] private Building hqBuilding;
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
                if(CanSpawnBuilding(activeBuildingType, UtilsClass.GetMouseWorldPosition(), out string errorMessage)){
                    if(ResourceManager.Instance.CanAfford(activeBuildingType.constructionResourceCostArray)){
                        ResourceManager.Instance.SpendResources(activeBuildingType.constructionResourceCostArray);
                        //Instantiate(activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), quaternion.identity);
                        BuildingConstruction.Create(UtilsClass.GetMouseWorldPosition(), activeBuildingType);
                    }
                    else{
                        TooltipUI.Instance.Show("Cannot afford " + activeBuildingType.GetConstructionResourceCostString(), new TooltipUI.TooltipTimer { timer = 2f});
                    }
                }
                else{
                    TooltipUI.Instance.Show(errorMessage, new TooltipUI.TooltipTimer { timer = 2f});
                } 
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

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage){
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3) boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 0;
        if(!isAreaClear){
            errorMessage = "Area is not clear!";
            return false;
        } 
        
        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);

        foreach (Collider2D collider2D in collider2DArray){
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null){
                //tem uma construção
                if(buildingTypeHolder.buildingType == buildingType){
                    //tem uma construção do mesmo tipo
                    errorMessage = "Too close to another building of the same type!";
                    return false;
                }
            }
        }   

        float maxConstructionRadius = 25f;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        
        foreach (Collider2D collider2D in collider2DArray){
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null){
                errorMessage = "";
                return true;
            }
        }   

        errorMessage = "To far from any other building!";
        return false;
    }

    public Building GetHQBuilding(){
        return hqBuilding;
    }
}
