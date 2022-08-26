using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] private SpawnController spawnController;
    [SerializeField] private GameController gameController;

    public void MoveForward()
    {
        if (gameController.isGameActive)
        {
            transform.position += spawnController.spaceBetweenCircles * transform.up;
            int parentIndex = transform.parent.GetSiblingIndex();
            Transform nextCircle = transform.parent.parent.GetChild(parentIndex + 1);
            transform.SetParent(nextCircle);



        }
    }


    public void OnJumpToGap()
    {
        gameController.GameOver();
        spawnController.StopSpawningObjects();
    }

    public void OnTriggerEnter(Collider other)
    {
        CirclePartBehaviour circlePart = other.gameObject.GetComponent<CirclePartBehaviour>();
        if (circlePart != null)
        {
            if (circlePart.isEmpty)
            {
                OnJumpToGap();
            }

        }

        Debug.Log("hit3");
    }

}
