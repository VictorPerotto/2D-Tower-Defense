using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour{

    [SerializeField] private Sprite arrowSprite;
    private Dictionary<BuildingTypeSO, Transform> buttonTransformDictionary;

    [SerializeField] Transform buttonTemplate;
    private Transform arrowButton;
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;

    private void Awake(){

        buttonTemplate.gameObject.SetActive(false);
    
        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);  
        buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;

        arrowButton = Instantiate(buttonTemplate, transform);
        arrowButton.gameObject.SetActive(true);

        float offsetAmount = +130f;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0f);

        arrowButton.Find("image").GetComponent<Image>().sprite = arrowSprite;
        arrowButton.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2 (60, 60);

        //() => {} cria uma shorthand para a criação de uma função, como se fosse uma função sem parametros
        arrowButton.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        MouseEnterExitEvents mouseEnterExitEvents = arrowButton.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>{
                TooltipUI.Instance.Show("Arrow");
            };

            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>{
                TooltipUI.Instance.Hide();
            };

        index ++;

        foreach(BuildingTypeSO buildingType in buildingTypeList.list){
            
            if(ignoreBuildingTypeList.Contains(buildingType)) continue;
            
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            offsetAmount = +130f;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0f);

            buttonTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            //() => {} cria uma shorthand para a criação de uma função, como se fosse uma função sem parametros
            buttonTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });
            
            mouseEnterExitEvents = buttonTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>{
                TooltipUI.Instance.Show(buildingType.nameString + "\n" + buildingType.GetConstructionResourceCostString());
            };

            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) =>{
                TooltipUI.Instance.Hide();
            };

            buttonTransformDictionary[buildingType] = buttonTransform;

            index ++;
        }   
    }

    private void Start(){
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e){
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton(){
        arrowButton.Find("selected").gameObject.SetActive(false);
        foreach(BuildingTypeSO buildingType in buttonTransformDictionary.Keys){
            Transform buttonTransform = buttonTransformDictionary[buildingType];
            buttonTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if(activeBuildingType == null){
            arrowButton.Find("selected").gameObject.SetActive(true);
        } 
        else{
            buttonTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
    }
}