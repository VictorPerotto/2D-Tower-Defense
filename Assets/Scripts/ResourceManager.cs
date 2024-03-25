using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceManager : MonoBehaviour{

    public static ResourceManager Instance {get; private set;}
    
    public event EventHandler OnResourceAmountChanged;
    //criamos um evento, que ativa um trigger quando chamado, e que pode ser ouvido por outra classe 

    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    private void Awake(){
        Instance = this;
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof (ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list){
            resourceAmountDictionary[resourceType] = 0;
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount){
        resourceAmountDictionary[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
        /*
            usar o ?.Invoke, faz a mesma função que fazer da seguinte forma:
            if (OnResourceAmountChanged != null){
            OnResourceAmountChanged(this,EventArgs.Empty)
        }*/ 
    }

    public int GetResourceAmount(ResourceTypeSO resourceType){
        return resourceAmountDictionary[resourceType];
    }
}
