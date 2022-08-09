using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject pivotObject;
    [SerializeField] private GameObject circlePrefab;
    public float spaceBetweenCircles = 0.8f;
    private float recentSpawnRadius;
    [SerializeField] private int spawnedCircleCount = 0;
    private int startingCirclePartCount = 8;
    private int startingStepCount;
    private int totalStepCount = 160;
    [SerializeField] private int currentDifficulty;
    private int startingDifficulty = 2;
    private int startingSpeed = 50;
    private Color colour;
    [SerializeField] private CharacterBehaviour characterBehaviour;
    [SerializeField] private Transform circleContainerTransform;
    private int currentCircle = 0;
    // Start is called before the first frame update
    void InstantiateStartingCircles(int count)
    {
        int currentSpeed = startingSpeed;
        for (int i = 0; i < count; i++)
        {


            currentDifficulty = startingDifficulty + spawnedCircleCount / 10;

            recentSpawnRadius += spaceBetweenCircles;

            GameObject circleObject = Instantiate(circlePrefab, circleContainerTransform);

            int direction;
            if (i % 2 == 1) direction = 1;
            else direction = -1;

            spawnedCircleCount++;

            int currentPartCount = startingCirclePartCount + spawnedCircleCount / 5;
            int currentStepCount = totalStepCount / currentPartCount + 1;
            circleObject.GetComponent<CircleBehaviour>().Initialize(recentSpawnRadius, currentPartCount, currentStepCount, pivotObject, currentSpeed, currentDifficulty, Color.green);
            currentSpeed = (startingSpeed + spawnedCircleCount / 10);
            currentSpeed *= direction;

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
        if (spawnedCircleCount < 10) colour = Color.green;
        else if (spawnedCircleCount >= 10 && spawnedCircleCount < 20) colour = Color.yellow;
        else colour = Color.red;

        currentDifficulty = startingDifficulty + spawnedCircleCount / 10;

        recentSpawnRadius += spaceBetweenCircles;

        GameObject circleObject = Instantiate(circlePrefab, circleContainerTransform);

        int direction = -1;
        if (spawnedCircleCount % 2 == 0) direction = 1;
        int currentSpeed = (startingSpeed + spawnedCircleCount / 10) * direction;

        int currentPartCount = startingCirclePartCount + spawnedCircleCount / 5;
        int currentStepCount = totalStepCount / currentPartCount + 1;
        circleObject.GetComponent<CircleBehaviour>().Initialize(recentSpawnRadius, currentPartCount, currentStepCount, pivotObject, currentSpeed, currentDifficulty, colour);
        spawnedCircleCount++;

        if (spawnedCircleCount > 15)
        {


            GameObject.Destroy(circleContainerTransform.GetChild(0).gameObject);

        }
    }
    private void Start()
    {
        InstantiateStartingCircles(7);
    }
    
}
