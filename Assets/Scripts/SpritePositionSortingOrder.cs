using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] float offsetAmountY;
    [SerializeField] bool runOnce;
    
    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate(){
        float precisionMultiplier = 5f;
        spriteRenderer.sortingOrder = (int) (-(transform.position.y + offsetAmountY) * precisionMultiplier);

        if(runOnce){
            Destroy(this);
        }
    }
}
