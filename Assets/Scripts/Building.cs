using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour{

    private HealthSystem healthSystem;

    private void Start(){
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.T)){
            healthSystem.Damage(999);
            Debug.Log(healthSystem.GetCurrentHealthAmount());
        }
    }

    private void HealthSystem_OnDied(object sender, EventArgs e){
        Destroy(gameObject);
    }
}
