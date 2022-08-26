using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {



        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            if (Input.touchCount > 0)
            {
                Touch[] touches = Input.touches;
                if (touches[0].phase == TouchPhase.Ended)
                {
                    gameController.OnScreenTouched();

                }

            }
        }



    }

}

