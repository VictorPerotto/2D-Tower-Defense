using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour{

    [SerializeField] private Transform demolishButton;
    private BuildingTypeSO buildingTypeSO;
    private HealthSystem healthSystem;

    private void Awake(){
        HideDemolishButton();
    }

    private void Start(){
        buildingTypeSO = GetComponent<BuildingTypeHolder>().buildingType;

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetMaxHealthAmount(buildingTypeSO.maxHealthAmount, true);
        healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void HealthSystem_OnDied(object sender, EventArgs e){
        Destroy(gameObject);
    }

    private void OnMouseEnter(){
        ShowDemolishButton();
    }

    private void OnMouseExit(){
        HideDemolishButton();
    }

    private void HideDemolishButton(){
        if(demolishButton != null){
            demolishButton.gameObject.SetActive(false);
        }
    }

    private void ShowDemolishButton(){
        if(demolishButton != null){
            demolishButton.gameObject.SetActive(true);
        }
    }
}
