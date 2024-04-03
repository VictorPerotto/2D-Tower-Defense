using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour{

    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform barTransform;
    void Start(){
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();

        barTransform = transform.Find("barPivot");
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    // Update is called once per frame
    void Update(){
        barTransform.localScale = new Vector3 (1 - resourceGenerator.GetTimerNormalized(), 0.5f, 1);
    }
}
