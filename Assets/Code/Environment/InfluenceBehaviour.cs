using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceBehaviour : MonoBehaviour
{
    public GameData data;
    public LineRenderer lineRenderer;
    public float radius = 1f; // Radius of the circle
    public int segments = 50;
    //Visual
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1; // Set the number of positions for the line renderer
        lineRenderer.useWorldSpace = true; // Use local space for positions

    }

    void Start()
    {
        data = GameData.instance;
        float angleStep = 360f / segments;
        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * (i * angleStep);
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, 0f, z));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfluenceArea(int amount)
    {
        radius = amount;
        float angleStep = 360f / segments;
        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * (i * angleStep);
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, 0f, z));
        }
    }
}
