using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass 
{
    private static Camera mainCamera;

    public static Vector3 GetMouseWorldPosition(){
        if(mainCamera == null) mainCamera = Camera.main;
        
        /*
            Input.mousePosition - retorna a posição do mouse em pixels da tela
            ScreenToWorldPoint - transforma uma posição em pixels da camera para uma posição em relação ao mundo 
        */
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    public static Vector3 GetRandomDir(){
        return new Vector3 (
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector){
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }
}
