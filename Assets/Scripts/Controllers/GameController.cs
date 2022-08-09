using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CharacterBehaviour characterBehaviour;
    [SerializeField] private SpawnController spawnController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnScreenTouched()
    {
        characterBehaviour.MoveForward();
        spawnController.SpawnCircle();
    }

}
