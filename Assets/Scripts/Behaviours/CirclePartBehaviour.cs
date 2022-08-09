using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePartBehaviour : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private MeshCollider meshCollider;
    private bool isEmpty;

    public void DrawCircle(float radius, float portion, int steps, bool isEmpty, Color colour)
    {

        transform.localPosition = Vector3.zero;
        lineRenderer.positionCount = steps;
        this.isEmpty = isEmpty;


        if (!isEmpty)
        {
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

                lineRenderer.material.color = colour;
            }
        }

        if (isEmpty)
        {

            Debug.Log("x\n");

            Mesh mesh = new Mesh();
            
            lineRenderer.BakeMesh(mesh, Camera.main);
            meshCollider.sharedMesh = mesh;


            meshCollider.convex = false;
            
        }

    }
}
