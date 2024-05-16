using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour{

    private GameObject spriteGameObject;
    private ResourceGeneratorNearby resourceGeneratorNearby;

    private void Awake(){
        spriteGameObject = transform.Find("sprite").gameObject;

        Hide();
    }

    private void Start(){
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;

        resourceGeneratorNearby = transform.Find("pfResourceGeneratorNearby").GetComponent<ResourceGeneratorNearby>();
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e){
        if(e.activeBuildingType == null){
            Hide();
            resourceGeneratorNearby.Hide();
        }
        else{
            Show(e.activeBuildingType.sprite);

            if (e.activeBuildingType.hasResourceGeneratorData){
                resourceGeneratorNearby.Show(e.activeBuildingType.resourceGeneratorData);
            } else {
                resourceGeneratorNearby.Hide();
            }
        }
    }

    private void Update(){
        transform.position = UtilsClass.GetMouseWorldPosition();
    }

    private void Show(Sprite ghostSprite){
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    private void Hide(){
        spriteGameObject.SetActive(false);
    }
}
