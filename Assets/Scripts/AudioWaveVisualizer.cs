using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(LineRenderer))]
public class AudioWaveVisualizer : MonoBehaviour
{
    public AudioManager audioManager;
    private LineRenderer lineRenderer;

    private int pointCount
    {
        get { return audioManager.sampleLength; }
    }

    private Camera cam;
    private float height;
    private float amplitude;
    private float width;

    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize * 2f;
        ;
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component is missing on the AudioWaveVisualizer GameObject.");
        }

        lineRenderer.positionCount = pointCount;
    }

    void Update()
    {
        amplitude = height / 2f * 0.8f;
        width = height * cam.aspect;

        UpdateWaveform();
    }

    void UpdateWaveform()
    {
        float[] samples = audioManager?.AudioSamples;
        if (samples == null || samples.Length == 0) return;

        for (int i = 0; i < pointCount; i++)
        {
            float x = ((float)i / (pointCount - 1)) * width - (width / 2f);
            float y = samples[i] * amplitude;
            Vector3 position = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, position);
        }
    }
}