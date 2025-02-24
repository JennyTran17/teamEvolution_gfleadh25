using UnityEngine;
using System.Collections.Generic;

public class LineManager : MonoBehaviour
{
    public Material lineMaterial;
    private LineRenderer currentLine;
    private Color currentColor;

    public float cellSize = 1f; 

    private List<Vector3> linePoints = new List<Vector3>();

    void StartLine(Vector3 startPosition, Color color)
    {
        currentColor = color;

        GameObject lineObject = new GameObject("Line");
        currentLine = lineObject.AddComponent<LineRenderer>();
        currentLine.material = lineMaterial;
        currentLine.startColor = color;
        currentLine.endColor = color;
        currentLine.startWidth = 0.1f;
        currentLine.endWidth = 0.1f;

        linePoints.Clear();
        linePoints.Add(SnapToGrid(startPosition)); // Add snapped start point
        UpdateLine();
    }

    void UpdateLine()
    {
        currentLine.positionCount = linePoints.Count;
        currentLine.SetPositions(linePoints.ToArray());
    }

    public void AddPoint(Vector3 newPoint)
    {
        Vector3 snappedPoint = SnapToGrid(newPoint);
        if (!linePoints.Contains(snappedPoint))
        {
            linePoints.Add(snappedPoint);
            UpdateLine();
        }
    }

    Vector3 SnapToGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / cellSize) * cellSize;
        float y = Mathf.Round(position.y / cellSize) * cellSize;
        return new Vector3(x, y, 0);
    }

    public void EndLine()
    {
        currentLine = null;
        linePoints.Clear();
    }
}
