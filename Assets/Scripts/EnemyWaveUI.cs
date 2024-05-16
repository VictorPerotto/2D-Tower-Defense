using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyWaveUI : MonoBehaviour{
   
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private TextMeshProUGUI waveCountdownText;

    private void Start(){
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
    }

    private void Update(){
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
