using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButton : MonoBehaviour{
    
    [SerializeField] Building building;
    [SerializeField] Button button;

    private void Awake(){
        button.onClick.AddListener(() => {
            BuildingTypeSO buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
            foreach(ResourceAmount resourceAmount in buildingType.constructionResourceCostArray){
                ResourceManager.Instance.AddResource(resourceAmount.resourceType, Mathf.FloorToInt(resourceAmount.amount * 0.6f));
            }
            Destroy(building.gameObject);
        });
    }
}
