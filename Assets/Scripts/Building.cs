using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour{

    private BuildingTypeSO buildingTypeSO;
    private HealthSystem healthSystem;

    private void Start(){
        buildingTypeSO = GetComponent<BuildingTypeHolder>().buildingType;

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetMaxHealthAmount(buildingTypeSO.maxHealthAmount, true);
        healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void HealthSystem_OnDied(object sender, EventArgs e){
        Destroy(gameObject);
    }
}
