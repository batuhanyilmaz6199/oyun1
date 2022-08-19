using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBehaviour : MonoBehaviour
{
    public float radius;
    private int stepCount;

    [SerializeField] private GameObject pivotObject;
    public float rotationSpeed;

    public int circlePartCount;
    [SerializeField] private GameObject circlePartPrefab;

    [SerializeField] private GameController gameController;

    private int random;

    private Color colour;

    private List<CirclePartBehaviour> circlePartBehaviours = new List<CirclePartBehaviour>();




    // Start is called before the first frame update
    public void Initialize(float radius, int circlePartCount, int stepCount, GameObject pivotObject, float rotationSpeed, int difficulty, Color colour, GameController gameController)
    {

        this.gameController = gameController;
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
            CirclePartBehaviour circlePartBehaviour = circlePart.GetComponent<CirclePartBehaviour>();
            circlePartBehaviours.Add(circlePartBehaviour); 
            circlePartBehaviour.DrawCircle(this.radius, (1f / this.circlePartCount), this.stepCount, isEmpty, colour, this);
            circlePart.transform.localRotation = Quaternion.Euler(0, 0, (360f / this.circlePartCount) * j);


        }
        transform.localPosition = Vector3.zero;

    }
   
    public void OnCharacterJumpOnTo()
    {
        Color color = gameController.colourOfTheCirclePlayerJumps;
        for(int i = 0; i < circlePartBehaviours.Count; i++)
        {
            circlePartBehaviours[i].ChangeCirclePartColor(color);
        }
    }
    void Update()
    {
        // transform.RotateAround(new Vector3(0, 0, 0), pivotObject.transform.position, rotationSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime * gameController.rotationSpeedMultiplier));
    }

   


}
