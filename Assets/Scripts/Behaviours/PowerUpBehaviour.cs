using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private CircleBehaviour circleBehaviour;
    [SerializeField] private GameController gameController;
    [HideInInspector] public PowerUpController powerUpController;
    [SerializeField] private GameObject slowMotion;
    [SerializeField] private GameObject invincible;
    [SerializeField] private GameObject easier;



    public void OnTriggerEnter(Collider other)
    {
        if (gameObject == slowMotion)
        {
            powerUpController.StartSpeedMultiplier();
            Debug.Log("slowDown");

        }
        if (gameObject == invincible)
        {
            powerUpController.StartMakePlayerInvincible();
            Debug.Log("invincible");

        }
        if (gameObject == easier)
        {
            powerUpController.StartMakeCirclesEasier();
            Debug.Log("easier");

        }

        Destroy(gameObject);
    }
    // Update is called once per frame
}
