using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position){
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private Rigidbody2D rigidbody2d;
    private Transform targetTransform;
    private HealthSystem healthSystem;
    private float lookForTargetsTimer;
    private float lookForTargetsTimerMax = 0.2f;
    
    // Start is called before the first frame update
    private void Start(){
        rigidbody2d = GetComponent<Rigidbody2D>();
        targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;

        lookForTargetsTimer = Random.Range(0f, lookForTargetsTimerMax);
    }

    private void Update(){
        HandleMovement();
        HandleTargeting();
    }

    private void HandleMovement(){
        if(targetTransform != null){
            Vector3 moveDirection = (targetTransform.position - transform.position).normalized;

            float moveSpeed = 6f;
            rigidbody2d.velocity = moveDirection * moveSpeed;
        } else {
            rigidbody2d.velocity = Vector2.zero;
        }
    }

    private void HandleTargeting(){
        lookForTargetsTimer -= Time.deltaTime;

        if (lookForTargetsTimer <= 0){
            lookForTargetsTimer = lookForTargetsTimerMax;
            LookForTargets();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        Building building = collision.gameObject.GetComponent<Building>();

        if (building != null){
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(gameObject);
        }
    }

    private void LookForTargets(){
        float targetMaxRadius = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach(Collider2D collider2D in collider2DArray){
            Building building = collider2D.GetComponent<Building>();
            if (building != null){
                if (targetTransform == null){
                    targetTransform = building.transform;
                } else {
                    if(Vector3.Distance(transform.position, building.transform.position) <
                    Vector3.Distance(transform.position, targetTransform.position)){
                        targetTransform = building.transform;
                    }
                }
            }
        }

        if (targetTransform == null){
           targetTransform =  BuildingManager.Instance.GetHQBuilding().transform;
        }
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e){
        Destroy(gameObject);
    }
}
