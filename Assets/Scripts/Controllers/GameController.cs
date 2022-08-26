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
    public bool isGamePaused;
    public bool isScoreMultiplierOn;
    public bool isPowerVacuumOn;

    public Button restartButton;
    public Button startButton;
    public Button pauseButton;
    public Button settings;
    public Button store;
    public Button sound;
    public Button menu;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextInGame;
    public TextMeshProUGUI totalPointsText;
    public TextMeshProUGUI priceOfDoublePointsText;
    public TextMeshProUGUI priceOfPowerVacuumText;

    public static int highestScore;
    public static int totalPoints;
    public static int priceOfDoublePoints = 500;
    public static int priceOfPowerVacuum = 1000;

    public Color colourOfTheCirclePlayerJumps;

    public GameObject gameOverScreen;
    public GameObject inGameScreen;
    public GameObject preGameScreen;
    public GameObject pauseScreen;
    public GameObject settingsScreen;
    public GameObject storeScreen;


    void Start()// baþlangýç
    {
        preGameScreen.SetActive(false);
        inGameScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        isPlayerInvincible = false;
        isGameActive = true;

        score = 0;
        scoreTextInGame.text = "Score:" + score;
        highestScore = PlayerPrefs.GetInt("highestScore", highestScore);
        totalPoints = PlayerPrefs.GetInt("totalPoints", totalPoints);
        totalPointsText.text = "Total Points: " + totalPoints;
        priceOfDoublePointsText.text = "PRICE: " + priceOfDoublePoints;
        priceOfPowerVacuumText.text = "PRICE: " + priceOfPowerVacuum;


    }

    public void OnScreenTouched()// ekrana dokununca çaðýrýlýr
    {
        if (!isGamePaused)
        {
            characterBehaviour.MoveForward();
            spawnController.SpawnCircle();
            score++;
            scoreTextInGame.text = "Score:" + score;
            spawnController.SpawnPowerUp();
        }
    }
    public void GameOver()
    {
        if (!isPlayerInvincible)
        {
            isPowerVacuumOn = false;
            isGameActive = false;
            isScoreMultiplierOn = false;
            gameOverScreen.SetActive(true);
            inGameScreen.SetActive(false);

            score--;

            AddPoints();
            SetHighestScore();

            highestScoreText.text = "Top Score:" + highestScore;
            scoreText.text = "Score:" + score;
            gameOverText.text = "GAME OVER";

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
        if (isScoreMultiplierOn)
        {
            totalPoints += score * 2;
            PlayerPrefs.SetInt("totalPoints", totalPoints);
        }
        else
        {
            totalPoints += score;
            PlayerPrefs.SetInt("totalPoints", totalPoints);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // loads current scene
        Time.timeScale = 1f;
        preGameScreen.SetActive(false);
        inGameScreen.SetActive(true);
    }
    public void PauseControl()
    {
        if (Time.timeScale == 1)
        {
            isGamePaused = true;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            inGameScreen.SetActive(false);
        }
        else
        {
            isGamePaused = false;
            pauseScreen.SetActive(false);
            inGameScreen.SetActive(true);
            Time.timeScale = 1;



        }
    }
    public void GoBackToHomeScreen()
    {
        pauseScreen.SetActive(false);
        preGameScreen.SetActive(true);
    }
    public void OpenSettings()
    {
        pauseScreen.SetActive(false);
        preGameScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }
    public void GoBackInUi()
    {
        settingsScreen.SetActive(false);
        if (isGamePaused)
        {
            pauseScreen.SetActive(true);
            isGamePaused = false;
        }
        else
        {
            preGameScreen.SetActive(true);
            storeScreen.SetActive(false);
            gameOverScreen.SetActive(false);
        }
    }
    public void OpenStore()
    {
        preGameScreen.SetActive(false);
        storeScreen.SetActive(true);
    }
    public void DoublePointsBought()
    {
        if (totalPoints >= priceOfDoublePoints || !isScoreMultiplierOn)
        {
            isScoreMultiplierOn = true;
            totalPointsText.text = "Total Points: " + totalPoints;

        }
    }
    public void PowerVacuumBought()
    {
        if (totalPoints >= priceOfPowerVacuum || !isPowerVacuumOn)
        {
            totalPoints -= priceOfPowerVacuum;
            isPowerVacuumOn = true;
            totalPointsText.text = "Total Points: " + totalPoints;

        }
    }

}
