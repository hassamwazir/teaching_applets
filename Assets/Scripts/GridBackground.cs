using UnityEngine;

public class GridBackground : MonoBehaviour
{
    public float duration = 10f;          // Duration in seconds (x-axis)
    public float amplitude = 1f;          // Maximum value on the y-axis
    public int gridLinesX = 10;           // Number of vertical grid lines
    public int gridLinesY = 5;            // Number of horizontal grid lines
    float gridLineWidth = 0.01f;   // Width of the grid lines

    void Start()
    {
        DrawGrid();
    }

    public void DrawGrid()
    {
        // Clear the grid before drawing
        ClearGrid();

        // Draw vertical grid lines
        for (int i = 0; i <= gridLinesX; i++)
        {
            float x = i * (duration / gridLinesX);
            CreateGridLine(new Vector3(x, 0, 0), new Vector3(x, amplitude, 0));
        }

        // Draw horizontal grid lines
        for (int j = 0; j <= gridLinesY; j++)
        {
            float y = j * (amplitude / gridLinesY);
            CreateGridLine(new Vector3(0, y, 0), new Vector3(duration, y, 0));
        }
    }

    void CreateGridLine(Vector3 start, Vector3 end)
    {
        GameObject gridLine = new GameObject("GridLine");
        LineRenderer lineRenderer = gridLine.AddComponent<LineRenderer>();
        lineRenderer.startWidth = gridLineWidth;
        lineRenderer.endWidth = gridLineWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Simple material
        lineRenderer.startColor = Color.gray;
        lineRenderer.endColor = Color.gray;
        gridLine.transform.parent = this.transform; // Set as child of GridBackground object
    }

    // clear the grid
    public void ClearGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
