using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject slowMotion;
    [SerializeField] private GameObject invincible;
    [SerializeField] private GameObject pivotObject;
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private GameObject easier;
    public float spaceBetweenCircles = 0.8f;
    [SerializeField] private float recentSpawnRadius;
    public int spawnedCircleCount;
    private int startingCirclePartCount = 8;
    private int startingStepCount;
    private int totalStepCount = 500;
    public int currentDifficulty;
    public int currentDifficultyMultiplier = 2;
    private int startingDifficulty = 3;
    private int startingSpeed = 50;
    private Color colour;
    [SerializeField] private CharacterBehaviour characterBehaviour;
    public Transform circleContainerTransform;
    private int currentCircle = 0;
    [SerializeField] private GameController gameController;
    [SerializeField] private PowerUpController powerUpController;
    private float linearSpeed = 1f;
    // Start is called before the first frame update
    private void Awake()
    {
        recentSpawnRadius = spaceBetweenCircles;

        linearSpeed = recentSpawnRadius * startingSpeed;
        Debug.Log("hesaplanan deger:" + linearSpeed);


    }
    private void Start()
    {
        Debug.Log("STARTA GIRDI");
        InstantiateStartingCircles(7);

    }



    void InstantiateStartingCircles(int count)
    {


        for (int i = 0; i < count; i++)
        {


            currentDifficulty = 1;


            GameObject circleObject = Instantiate(circlePrefab, circleContainerTransform);

            int direction = -1;
            if (i % 2 == 1) direction = 1;


            spawnedCircleCount++;

            int currentPartCount = startingCirclePartCount + spawnedCircleCount / 3;
            int currentStepCount = totalStepCount / currentPartCount + 1;
            float currentSpeed = direction * (((linearSpeed / recentSpawnRadius)) + currentDifficulty * 10 * currentDifficultyMultiplier);


            circleObject.GetComponent<CircleBehaviour>().Initialize(recentSpawnRadius, currentPartCount, currentStepCount, pivotObject, currentSpeed, currentDifficulty * currentDifficultyMultiplier, Color.green, gameController);

            recentSpawnRadius += spaceBetweenCircles;
        }
        Transform firstCircle = circleContainerTransform.GetChild(0);

        CircleBehaviour firstCircleBehaviour = firstCircle.GetComponent<CircleBehaviour>();
        characterBehaviour.transform.SetParent(firstCircle, true);
        characterBehaviour.transform.localPosition = new Vector3(0, firstCircleBehaviour.radius, 0);
        characterBehaviour.transform.rotation = Quaternion.Euler(Vector3.zero);

        //Vector3 targetPos = pivotObject.transform.position;
        //Vector3 charPos = characterBehaviour.transform.position;
        //Vector3 dir = pivotObject.transform.position - characterBehaviour.transform.position;

        //targetPos.x = targetPos.x - charPos.x;
        //targetPos.y = targetPos.y - charPos.y;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //characterBehaviour.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle ));
        //characterBehaviour.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //characterBehaviour.transform.LookAt(pivotObject.transform, Vector3.forward);

        //characterBehaviour.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 315));
        //characterBehaviour.transform.position = new Vector3(0.5f, 0.5f, -1);
    }


    // Update is called once per frame
    public void SpawnCircle()
    {
        if (gameController.isGameActive)
        {
            colour = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

            currentDifficulty = startingDifficulty + spawnedCircleCount / 10;


            GameObject circleObject = Instantiate(circlePrefab, circleContainerTransform);





            int direction = -1;
            if (spawnedCircleCount % 2 == 1) direction = 1;
            float currentSpeed = direction * (((linearSpeed / recentSpawnRadius)) + currentDifficulty * 10 * currentDifficultyMultiplier);

            int currentPartCount = startingCirclePartCount + spawnedCircleCount / 5;
            int currentStepCount = totalStepCount / currentPartCount + 1;
            circleObject.GetComponent<CircleBehaviour>().Initialize(recentSpawnRadius, currentPartCount, currentStepCount, pivotObject, currentSpeed, currentDifficulty, colour, gameController);
            spawnedCircleCount++;

            if (spawnedCircleCount > 15)
            {


                GameObject.Destroy(circleContainerTransform.GetChild(0).gameObject);

            }
            recentSpawnRadius += spaceBetweenCircles;
        }

    }
    public void SpawnPowerUp()
    {

        bool containsPowerUp = false;
        int random, random1;
        
        if (!gameController.isPowerVacuumOn)
        {
            random = Random.Range(1, 10);
            random1 = Random.Range(1, 10);
            if (random == random1)
            {
                containsPowerUp = true;
            }
        }
        else
        {
            random = Random.Range(1, 2); 
            random1 = Random.Range(1, 2);
            if (random == random1)
            {
                containsPowerUp = true;
            }
        }



        int lastChild = circleContainerTransform.childCount - 1;
        Debug.Log(lastChild);
        Transform currentCircle = circleContainerTransform.GetChild(lastChild);

        CircleBehaviour currentCircleBehaviour = currentCircle.GetComponent<CircleBehaviour>();


        if (containsPowerUp)
        {
            int randomPowerUp = Random.Range(1, 3);
            if (random == 1)
            {
                PowerUpBehaviour powerUpBehaviour = Instantiate(slowMotion, new Vector3(0, currentCircleBehaviour.radius, 0), Quaternion.Euler(Vector3.zero), currentCircle).GetComponent<PowerUpBehaviour>();
                powerUpBehaviour.powerUpController = powerUpController;
            }
            if (random == 2)
            {
                PowerUpBehaviour powerUpBehaviour = Instantiate(invincible, new Vector3(0, currentCircleBehaviour.radius, 0), Quaternion.Euler(Vector3.zero), currentCircle).GetComponent<PowerUpBehaviour>();
                powerUpBehaviour.powerUpController = powerUpController;
            }
            if (random == 3)
            {
                PowerUpBehaviour powerUpBehaviour = Instantiate(easier, new Vector3(0, currentCircleBehaviour.radius, 0), Quaternion.Euler(Vector3.zero), currentCircle).GetComponent<PowerUpBehaviour>();
                powerUpBehaviour.powerUpController = powerUpController;
            }
        }
    }
    public void StopSpawningObjects()
    {
        if (!gameController.isGameActive)
        {


            gameController.rotationSpeedMultiplier = 0;

        }
    }

}
