using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public MicrophoneInput micInput;
    public int SampleLength => micInput.sampleLength; // Number of samples to extract per frame

    // Reference level for dB calculation (full scale digital audio)
    private const float DBRef = 1.0f;

    // Minimum dB value to display (below this will be considered silence)
    public const float MinimumDB = -80.0f;

    // Audio metrics
    public float CurrentVolumeDb { get; private set; }
    public float[] AudioSamples => micInput?.samples;

    private void Update()
    {
        UpdateAudioMetrics();
    }

    private void UpdateAudioMetrics()
    {
        var samples = AudioSamples;
        if (samples == null || samples.Length == 0) return;

        // Calculate volume in dB
        CurrentVolumeDb = CalculateVolumeDb(samples);
    }

    private static float CalculateVolumeDb(float[] samples)
    {
        // Calculate RMS (Root Mean Square) amplitude
        float sum = 0;
        for (var i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }

        var rms = Mathf.Sqrt(sum / samples.Length);

        // Convert RMS to dB
        var db = 20 * Mathf.Log10(rms / DBRef);

        // Clamp to minimum value to avoid -Infinity for silence
        return Mathf.Max(db, MinimumDB);
    }
}