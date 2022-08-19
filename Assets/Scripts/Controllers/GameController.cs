using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private CharacterBehaviour characterBehaviour;
    [SerializeField] private SpawnController spawnController;
    [SerializeField] public static int score = 0;
    [HideInInspector] public float rotationSpeedMultiplier = 1;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI highestScoreText;
    public bool isGameActive;
    public bool isPlayerInvincible;
    public Button restartButton;
    public TextMeshProUGUI scoreText;
    public static int highestScore;
    public static int totalPoints;
    public Color colourOfTheCirclePlayerJumps;
    public GameObject gameOverScreen;
    void Start()
    {
        isPlayerInvincible = false;
        score = 0;
        gameOverScreen.SetActive(false);
        isGameActive = true;
        highestScore = PlayerPrefs.GetInt("highestScore", highestScore);
        totalPoints = PlayerPrefs.GetInt("totalPoints", totalPoints);

    }

    public void OnScreenTouched()
    {
        characterBehaviour.MoveForward();
        spawnController.SpawnCircle();
        score++;
        spawnController.SpawnPowerUp();
    }
    public void GameOver()
    {
        if (!isPlayerInvincible)
        {
            AddPoints();
            SetHighestScore();
            highestScoreText.text = "Top Score:" + highestScore;
            scoreText.text = "Score:" + score;
            isGameActive = false;
            gameOverText.text = "GAME OVER";
            gameOverScreen.SetActive(true);
            score = 0;


        }
    }
    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // loads current scene
        Time.timeScale = 1f;
    }
    private void SetHighestScore()
    {
        if (score > highestScore)
        {
            highestScore = score;
            PlayerPrefs.SetInt("highestScore", highestScore);
        }
    }
    private void AddPoints()
    {
        totalPoints += score;
        PlayerPrefs.SetInt("totalPoints", totalPoints);
    }
}
