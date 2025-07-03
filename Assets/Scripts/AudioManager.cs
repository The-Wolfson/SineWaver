using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public MicrophoneInput micInput;
    public int sampleLength => micInput.sampleLength; // Number of samples to extract per frame

    // Reference level for dB calculation (full scale digital audio)
    private const float DB_REF = 1.0f;

    // Minimum dB value to display (below this will be considered silence)
    public static readonly float MinimumDB = -80.0f;

    // Audio metrics
    public float CurrentVolumeDb { get; private set; }
    public float CurrentDominantFrequency { get; private set; }
    public float[] AudioSamples => micInput?.samples;

    // Events
    public event Action<float> OnVolumeChanged;

    private void Update()
    {
        UpdateAudioMetrics();
    }

    private void UpdateAudioMetrics()
    {
        float[] samples = AudioSamples;
        if (samples == null || samples.Length == 0) return;

        // Calculate volume in dB
        float volumeDb = CalculateVolumeDb(samples);
        if (volumeDb != CurrentVolumeDb)
        {
            CurrentVolumeDb = volumeDb;
            OnVolumeChanged?.Invoke(CurrentVolumeDb);
        }
    }

    public float CalculateVolumeDb(float[] samples)
    {
        // Calculate RMS (Root Mean Square) amplitude
        float sum = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }

        float rms = Mathf.Sqrt(sum / samples.Length);

        // Convert RMS to dB
        float db = 20 * Mathf.Log10(rms / DB_REF);

        // Clamp to minimum value to avoid -Infinity for silence
        return Mathf.Max(db, MinimumDB);
    }
}