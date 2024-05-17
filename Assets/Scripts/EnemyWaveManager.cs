using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyWaveManager : MonoBehaviour{
    
    private enum State{
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    public event EventHandler OnWaveNumberChanged;

    [SerializeField] private List<Transform> enemySpawnPositionList;
    [SerializeField] private Transform nextWaveSpawnPosition;
    [SerializeField] private float nextWaveSpawnTimerMax;
    private int remainingEnemySpawnCount;
    private int waveNumber;
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private Vector3 spawnPosition;
    private State state;
    
    private void Start(){
        state = State.WaitingToSpawnNextWave;
        spawnPosition = enemySpawnPositionList[UnityEngine.Random.Range(0, enemySpawnPositionList.Count)].position;
        nextWaveSpawnPosition.position = spawnPosition;
    }

    private void Update(){
        switch(state){
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if(nextWaveSpawnTimer <= 0){
                    SpawnWave();
                }
            break;

            case State.SpawningWave:
                if (remainingEnemySpawnCount > 0){
                    nextEnemySpawnTimer -= Time.deltaTime;

                    if(nextEnemySpawnTimer <= 0){
                        nextEnemySpawnTimer = UnityEngine.Random.Range(0f, 0.2f);
                        Enemy.Create(spawnPosition + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(0f, 10f));
                        remainingEnemySpawnCount--;
                    }
                    
                    //spawned all the enemys
                    if(remainingEnemySpawnCount <= 0){
                        state = State.WaitingToSpawnNextWave;
                        spawnPosition = enemySpawnPositionList[UnityEngine.Random.Range(0, enemySpawnPositionList.Count)].position;
                        nextWaveSpawnPosition.position = spawnPosition;
                        nextWaveSpawnTimer = nextWaveSpawnTimerMax;
                    }
                }
            break;

            
        }
    }

    private void SpawnWave(){
        remainingEnemySpawnCount = 5 + (3 * waveNumber);
        state = State.SpawningWave;
        waveNumber ++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
        /*
            if(OnWaveNumberChanged != null){
                OnWaveNumberChanged(this, EventArgs.Empty);
            }
            verifica se tem algum subscriber para esse evento
        */
    }

    public int GetWaveNumber(){
        return waveNumber;
    }
    
    public float GetWaveCountdown(){
        return nextWaveSpawnTimer;;
    }

    public Vector3 GetSpawnPosition(){
        return spawnPosition;
    }
}
