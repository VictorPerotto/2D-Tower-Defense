using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour{

    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingTypeSO){
        Transform pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
        Transform buildingConstructionTransform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingTypeSO);

        return buildingConstruction;
    }

    [SerializeField] SpriteRenderer spriteRenderer;
    private BuildingTypeSO buildingTypeSO;
    private BoxCollider2D boxCollider2D;
    private BuildingTypeHolder buildingTypeHolder;
    private Material constructionMaterial;
    private float constructionTimer;
    private float constructionTimerMax;

    private void Awake(){
        boxCollider2D = GetComponent<BoxCollider2D>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        constructionMaterial = spriteRenderer.material;
    }

    void Update(){
        constructionTimer -= Time.deltaTime;

        constructionMaterial.SetFloat("_Progress", GetTimerNormalized());
        if(constructionTimer <= 0){
            Instantiate(buildingTypeSO.prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void SetBuildingType(BuildingTypeSO buildingTypeSO){
        this.buildingTypeSO = buildingTypeSO;

        constructionTimerMax = buildingTypeSO.constructionTimerMax;
        constructionTimer = constructionTimerMax;
        
        spriteRenderer.sprite = buildingTypeSO.sprite;

        boxCollider2D.offset = buildingTypeSO.prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingTypeSO.prefab.GetComponent<BoxCollider2D>().size;

        buildingTypeHolder.buildingType = buildingTypeSO;
    }

    public float GetTimerNormalized(){
        return 1 - constructionTimer/constructionTimerMax;
    }
}
