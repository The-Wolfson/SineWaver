using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class UIUpdater : MonoBehaviour
{
    public AudioManager audioManager;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI scoreText;
    
    private void Start()
    {
        // Subscribe to audio events
        audioManager.OnVolumeChanged += OnVolumeChanged;
    }

    private void OnDestroy()
    {
        audioManager.OnVolumeChanged -= OnVolumeChanged;
    }

    private void OnVolumeChanged(float volumeDb)
    {
        UpdateVolumeText(volumeDb);
    }

    private void UpdateVolumeText(float volumeDb)
    {
        volumeText.text = FormatVolumeText(volumeDb);
    }

    private static string FormatVolumeText(float volumeDb)
    {
        return volumeDb < AudioManager.MinimumDB ? "-∞ dB" : $"{volumeDb:F1} dB";
    }
}