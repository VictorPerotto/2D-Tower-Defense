using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour{

    private Enemy targetEnemy;
    private float lookForTargetsTimer;
    private float lookForTargetsTimerMax = 0.2f;
    private float shootTimer;
    [SerializeField] private Transform arrowSpawnTransform;
    [SerializeField] private float shootTimerMax;

    private void Update(){
        HandleTargeting();
        HandleShooting();
    }

    private void HandleTargeting(){
        lookForTargetsTimer -= Time.deltaTime;

        if (lookForTargetsTimer <= 0){
            lookForTargetsTimer = lookForTargetsTimerMax;
            LookForTargets();
        }
    }

    private void HandleShooting(){
        shootTimer -= Time.deltaTime;

        if(shootTimer <= 0){
            shootTimer += shootTimerMax;      
            if(targetEnemy != null){
                ArrowProjectile.Create(arrowSpawnTransform.position, targetEnemy);
            }

        }
    }

    private void LookForTargets(){
        float targetMaxRadius = 20f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach(Collider2D collider2D in collider2DArray){
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null){
                if (targetEnemy == null){
                    targetEnemy = enemy;
                } else {
                    if(Vector3.Distance(transform.position, enemy.transform.position) <
                    Vector3.Distance(transform.position, targetEnemy.transform.position)){
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }
}
