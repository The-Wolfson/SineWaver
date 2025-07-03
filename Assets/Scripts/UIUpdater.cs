using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    public AudioManager audioManager;
    public TextMeshProUGUI volumeText;

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
        if (volumeDb < AudioManager.MinimumDB)
        {
            return "-∞ dB";
        }
        else
        {
            return $"{volumeDb:F1} dB";
        }
    }
}