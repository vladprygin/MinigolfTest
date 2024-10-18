using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerIndicatorController : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    public void ShowLineRenderer()
    {
        _lineRenderer.gameObject.SetActive(true);
    }

    public void UpdateLineRendererPoints(Vector3[] points)
    {
        _lineRenderer.SetPositions(points);
    }

    public void HideLineRenderer()
    {
        _lineRenderer.gameObject.SetActive(false);
    }
    
}
