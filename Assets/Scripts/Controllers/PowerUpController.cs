using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour    
{
    private float durationOfPowerUp = 12f; 
    [SerializeField] private GameController gameController;
    [SerializeField] private SpawnController spawnController;
    private IEnumerator SpeedMultiplier()
    {
        gameController.rotationSpeedMultiplier /= 2;
        yield return new WaitForSeconds(durationOfPowerUp);
        gameController.rotationSpeedMultiplier *= 2;
    }
    private IEnumerator MakePlayerInvincible()
    {
        gameController.isPlayerInvincible = true;
        yield return new WaitForSeconds(durationOfPowerUp);
        gameController.isPlayerInvincible = false;
    }
    private IEnumerator MakeCirclesEasier()
    {
        spawnController.currentDifficultyMultiplier = 1;
        yield return new WaitForSeconds(durationOfPowerUp);
        spawnController.currentDifficultyMultiplier = 2;

    }
    public void StartSpeedMultiplier()
    {
        StartCoroutine(SpeedMultiplier());
    }
    public void StartMakePlayerInvincible()
    {
        StartCoroutine(MakePlayerInvincible());
    }
    public void StartMakeCirclesEasier()
    {
        StartCoroutine(MakeCirclesEasier());
    }
}
