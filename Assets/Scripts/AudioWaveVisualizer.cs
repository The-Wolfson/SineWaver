using UnityEngine;

/// <summary>
/// Visualizes audio waveforms using a LineRenderer.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class AudioWaveVisualizer : MonoBehaviour
{
    /// <summary>
    /// Reference to the AudioManager to get audio samples.
    /// </summary>
    public AudioManager audioManager;
    
    // The LineRenderer component to draw the waveform.
    private LineRenderer _lineRenderer;

    /// <summary>
    /// The number of points in the waveform, determined by the audio sample length.
    /// </summary>
    private int PointCount => audioManager.SampleLength;

    // The main camera.
    private Camera _cam;

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void Start()
    {
        _cam = Camera.main;
        _lineRenderer = GetComponent<LineRenderer>();
        // Set the number of points in the LineRenderer.
        _lineRenderer.positionCount = PointCount;
    }

    /// <summary>
    /// Called every frame to update the waveform visualization.
    /// </summary>
    private void Update()
    {
        // Get the screen dimensions.
        var height = _cam.orthographicSize * 0.8f; // 80% of viewport height
        var width = height * _cam.aspect;
        
        // Update the waveform display.
        UpdateWaveform(width, height);
    }

    /// <summary>
    /// Updates the waveform based on the audio samples.
    /// </summary>
    /// <param name="width">The width of the display area.</param>
    /// <param name="height">The height of the display area.</param>
    void UpdateWaveform(float width, float height)
    {
        var samples = audioManager?.AudioSamples;
        if (samples == null || samples.Length == 0) return;

        // Iterate through the audio samples to set the points of the LineRenderer.
        for (var i = 0; i < PointCount; i++)
        {
            // Calculate the position of each point in the waveform.
            // Calculate the x-coordinate by mapping the sample index (i) to a position within the display width
            var x = ((float)i / (PointCount - 1)) * width - (width / 2f);
            var y = samples[i] * height;
            var position = new Vector3(x, y, 0);
            
            // Set the position of the point in the LineRenderer.
            _lineRenderer.SetPosition(i, position);
        }
    }
}