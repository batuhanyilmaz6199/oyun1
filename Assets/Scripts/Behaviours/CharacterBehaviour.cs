using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] private SpawnController spawnController;
   
    // Start is called before the first frame update
    void Start()
    {


    }

    public void MoveForward()
    {

        transform.position += spawnController.spaceBetweenCircles * transform.up;
        int parentIndex =  transform.parent.GetSiblingIndex();
        Transform nextCircle = transform.parent.parent.GetChild(parentIndex + 1);
        transform.SetParent(nextCircle);
        
    }
}
