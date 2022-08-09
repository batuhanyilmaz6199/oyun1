using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBehaviour : MonoBehaviour
{
    [HideInInspector] public float radius;
    private int stepCount;

    [SerializeField] private GameObject pivotObject;
    [SerializeField] private int rotationSpeed;

    private int circlePartCount;
    [SerializeField] private GameObject circlePartPrefab;

    private int random;

    private Color colour;

    // Start is called before the first frame update
    public void Initialize(float radius, int circlePartCount, int stepCount, GameObject pivotObject, int rotationSpeed, int difficulty, Color colour)
    {
        this.colour = colour;
        this.radius = radius;
        this.circlePartCount = circlePartCount;
        this.stepCount = stepCount;
        this.rotationSpeed = rotationSpeed;
        this.pivotObject = pivotObject;

        for (int j = 0; j < this.circlePartCount; j++)
        {
            

            random = Random.Range(1, difficulty);
            bool isEmpty = true;
            if (j % random == 0)
            {
                isEmpty = false;
            }
            GameObject circlePart = Instantiate(circlePartPrefab, transform);
            circlePart.transform.position = Vector2.zero;
            circlePart.GetComponent<CirclePartBehaviour>().DrawCircle(this.radius, (1f / this.circlePartCount), this.stepCount, isEmpty, colour);
            circlePart.transform.localRotation = Quaternion.Euler(0, 0, (360f / this.circlePartCount) * j);

            
        }
        transform.localPosition = Vector3.zero;

    }
    void Update()
    {
        // transform.RotateAround(new Vector3(0, 0, 0), pivotObject.transform.position, rotationSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }




}
