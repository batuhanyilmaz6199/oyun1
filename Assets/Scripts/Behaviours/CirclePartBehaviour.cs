using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePartBehaviour : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private CircleBehaviour circleBehaviour;
    [SerializeField] private SpawnController spawnController;


    public bool isEmpty;

    public bool containsPowerUp;

    private Color colour;

    public int steps;
    public void DrawCircle(float radius, float portion, int steps, bool isEmpty, Color colour, CircleBehaviour circleBehaviour)
    {
        this.circleBehaviour = circleBehaviour;
        this.colour = colour;
        transform.localPosition = Vector3.zero;
        lineRenderer.positionCount = steps;
        this.isEmpty = isEmpty;
        this.steps = steps;




        for (int i = 0; i < steps; i++)
        {

            float circumferenceProgress = (float)i / steps;
            float currentRadian = circumferenceProgress * portion * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, currentPosition);


        }
        lineRenderer.material.color = colour;
        lineRenderer.material.shader = Shader.Find("Unlit/Color");






        Mesh mesh = new Mesh();

        lineRenderer.BakeMesh(mesh, Camera.main);
        meshCollider.sharedMesh = mesh;




        meshCollider.convex = false;
        if (isEmpty)
        {
            lineRenderer.enabled = false;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isEmpty)
        {
            circleBehaviour.OnCharacterJumpOnTo();
            Debug.Log("renk1");
        }
    }


    public void ChangeCirclePartColor(Color color)
    {
        lineRenderer.material.color = color;
    }




}
