using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] Transform barTransform;

    // Start is called before the first frame update
    void Start(){
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e){
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void UpdateBar(){
        barTransform.localScale = new Vector3(healthSystem.GetCurrentHealthAmountNormalized(), 1, 1);
    }

    private void UpdateHealthBarVisible(){
        if(healthSystem.IsFullHealth()){
            gameObject.SetActive(false);
        } else{
            gameObject.SetActive(true);
        }
    }
}
