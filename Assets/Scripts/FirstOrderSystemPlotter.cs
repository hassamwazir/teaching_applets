using UnityEngine;

public class FirstOrderSystemPlotter : MonoBehaviour
{
    public float dcGain = 1.0f;         // Final value of the response
    public float timeConstant = 2.0f;      // Time constant τ
    public int resolution = 500;       // Number of points in the wave
    public float duration = 10.0f;         // Duration in seconds
    public float lineWidth = 0.05f;      // Width of the plot line
    private LineRenderer plotLineRenderer;
    private float last_dc_gain = 1.0f;

    GridBackground gridBackground;

    void Start()
    {
        gridBackground = GetComponent<GridBackground>();

        // Create and configure the plot line
        plotLineRenderer = gameObject.AddComponent<LineRenderer>();
        plotLineRenderer.startWidth = lineWidth;
        plotLineRenderer.endWidth = lineWidth;
        plotLineRenderer.positionCount = resolution + 1;


        DrawFirstOrderResponse();
    }

    void DrawFirstOrderResponse()
    {
        // if the duration is less than 0, set it to 0
        if (duration < 0)
        {
            duration = 0;
        }

        if (timeConstant <= 0)
        {
            timeConstant = 0.001f;
        }

        if (dcGain <= 0)
        {
            dcGain = 0.001f;
        }


        // Calculate the number of points based on the duration
        int pointsPerSecond = 100;
        int resolution = Mathf.CeilToInt(pointsPerSecond * duration);



        
        // Adjust the LineRenderer position count based on the new resolution
        plotLineRenderer.positionCount = resolution + 1;

        float timeStep = duration / resolution;

        for (int i = 0; i <= resolution; i++)
        {
            float t = i * timeStep;
            float y = dcGain * (1 - Mathf.Exp(-t / timeConstant));
            plotLineRenderer.SetPosition(i, new Vector3(t, y, 0));
        }

        // draw a horizontal line at 0,5 and 5,5
       gridBackground.CreateGridLine(new Vector3(0, 0.5f, 0), new Vector3(10, 0.5f, 0));

    }

    // Update the dcGain when the slider value changes
    public void DCGainChanged(float newDCGain)
    {
        dcGain = newDCGain;
        DrawFirstOrderResponse();
    }

    public void TimeConstantChanged(float newTimeConstant)
    {
        timeConstant = newTimeConstant;
        DrawFirstOrderResponse();
    }

    void Update()
    {
        // if (last_dc_gain != dcGain)
        // {
        //     // Calculate the amplitude based on the dc gain. amplitudeshould be a multiple of 0.2f
        //     float amplitude = Mathf.Ceil(dcGain / 0.5f) * 0.5f;
        //     gridBackground.amplitude = amplitude;

        //     // calculate the gridlinesx based on the new duration
        //     gridBackground.gridLinesX = Mathf.CeilToInt(duration / 2f);


        //     // the grid lines based on the new amplitude
        //     gridBackground.gridLinesY = Mathf.CeilToInt(amplitude / 0.5f);

        //     // Draw the grid again
        //     gridBackground.DrawGrid();
        // }
        // last_dc_gain = dcGain;


        // Optional: animate parameters or redraw
        // DrawFirstOrderResponse();
    }

    // Draw horizintal and vertical dashed lines once, and then update their positions
    public void DrawLines()
    {
        // Clear the grid before drawing
        gridBackground.ClearGrid();

        // Draw vertical grid lines
        for (int i = 0; i <= gridBackground.gridLinesX; i++)
        {
            float x = i * (duration / gridBackground.gridLinesX);
            gridBackground.CreateGridLine(new Vector3(x, 0, 0), new Vector3(x, gridBackground.amplitude, 0));
        }

        // Draw horizontal grid lines
        for (int j = 0; j <= gridBackground.gridLinesY; j++)
        {
            float y = j * (gridBackground.amplitude / gridBackground.gridLinesY);
            gridBackground.CreateGridLine(new Vector3(0, y, 0), new Vector3(duration, y, 0));
        }
    }

    // Clear the grid
    public void ClearGrid()
    {
        gridBackground.ClearGrid();
    }
}
