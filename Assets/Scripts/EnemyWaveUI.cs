using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyWaveUI : MonoBehaviour{
   
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private TextMeshProUGUI waveCountdownText;
    [SerializeField] private RectTransform nextWaveSpawnIndicator;
    [SerializeField] private RectTransform closestEnemyIndicator;
    private Camera mainCamera;

    private void Start(){
        mainCamera = Camera.main;
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
    }

    private void Update(){
        HandleMessage();
        HandleNextSpawnPositionIndicator();
        HandleClosestEnemyIndicator();
    }

    private void HandleNextSpawnPositionIndicator(){
        Vector3 dirToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;

        nextWaveSpawnIndicator.anchoredPosition = dirToNextSpawnPosition * 300f;
        nextWaveSpawnIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToNextSpawnPosition));

        float distanceToNextSpawnPostion = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        nextWaveSpawnIndicator.gameObject.SetActive(distanceToNextSpawnPostion > mainCamera.orthographicSize * 1.5);
    }

    private void HandleClosestEnemyIndicator(){

        float targetMaxRadius = 9999f;
        Transform hqTransform = BuildingManager.Instance.GetHQBuilding().transform;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(hqTransform.position, targetMaxRadius);
        Enemy targetEnemy = null;
        
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

        if(targetEnemy != null){
            Vector3 dirToClosestEnemy = (targetEnemy.transform.position - mainCamera.transform.position).normalized;

            closestEnemyIndicator.anchoredPosition = dirToClosestEnemy * 200f;
            closestEnemyIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToClosestEnemy));

            float distanceToClosestEnemy = Vector3.Distance(targetEnemy.transform.position, mainCamera.transform.position);
            closestEnemyIndicator.gameObject.SetActive(distanceToClosestEnemy > mainCamera.orthographicSize * 1.5);
        } else {
            closestEnemyIndicator.gameObject.SetActive(false);
        }
    }
    
    private void HandleMessage(){

        float waveCountdown = enemyWaveManager.GetWaveCountdown();

        if (waveCountdown <= 0){
            SetWaveCountdownText("");
        } else {
            SetWaveCountdownText("Next wave in " + waveCountdown.ToString("F1") + "s");
        }
    }

    private void SetWaveNumberText(string waveNumberText){
        this.waveNumberText.SetText(waveNumberText);
    }

    private void SetWaveCountdownText(string waveCountdownText){
        this.waveCountdownText.SetText(waveCountdownText);
    }
    
    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e){
        int waveNumber = enemyWaveManager.GetWaveNumber();
        SetWaveNumberText("Wave " + waveNumber);
    }
}
