using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour{
    
    [SerializeField] int maxHealthAmount;
    private int currentHealthAmount;

    public event EventHandler OnDamaged;
    public event EventHandler OnDied;

    private void Awake(){
        currentHealthAmount = maxHealthAmount;
    }

    public void Damage(int damage){
        currentHealthAmount -= damage;
        currentHealthAmount = Mathf.Clamp(currentHealthAmount, 0, maxHealthAmount);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if(IsDead()){
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsDead(){
        return currentHealthAmount == 0;
    }

    public bool IsFullHealth(){
        return currentHealthAmount == maxHealthAmount;
    }

    public int GetCurrentHealthAmount(){
        return currentHealthAmount;
    }

    public float GetCurrentHealthAmountNormalized(){
        return (float) currentHealthAmount / maxHealthAmount;
    }

    public void SetMaxHealthAmount(int maxHealthAmount, bool updateHealthAmount){
        this.maxHealthAmount = maxHealthAmount;

        if(updateHealthAmount){
            currentHealthAmount = maxHealthAmount;
        }
    }
}
