using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] private int startingRotationSpeed = 50;
    [SerializeField] private int currentRotationSpeed;
    [SerializeField] private int score = 0;
    [SerializeField] private GameObject pivotObject;

    [SerializeField] private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {


    }
    
    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.touchCount > 0)
        {
            Touch[] touches = Input.touches;
            if (touches[0].phase == TouchPhase.Ended)
            {

                gameController.OnScreenTouched();
                //score++;
            }
        }
        
        
    }

}

