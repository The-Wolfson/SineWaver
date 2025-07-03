using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AudioWaveVisualizer : MonoBehaviour
{
    public AudioManager audioManager;
    private LineRenderer _lineRenderer;

    private int PointCount => audioManager.SampleLength;

    private Camera _cam;
    private float _height;
    private float _amplitude;
    private float _width;

    private void Start()
    {
        _cam = Camera.main;
        if (_cam != null) _height = _cam.orthographicSize * 2f;
        
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.positionCount = PointCount;
    }

    private void Update()
    {
        _amplitude = _height / 2f * 0.8f;
        _width = _height * _cam.aspect;

        UpdateWaveform();
    }

    void UpdateWaveform()
    {
        var samples = audioManager?.AudioSamples;
        if (samples == null || samples.Length == 0) return;

        for (var i = 0; i < PointCount; i++)
        {
            var x = ((float)i / (PointCount - 1)) * _width - (_width / 2f);
            var y = samples[i] * _amplitude;
            var position = new Vector3(x, y, 0);
            _lineRenderer.SetPosition(i, position);
        }
    }
}