using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    public AudioManager audioManager;
    public ScoreManager scoreManager;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI targetVolumeText;
    public TextMeshProUGUI scoreText;

    
    private void Update()
    {
        // Update the score text
        scoreText.text = scoreManager.Score.ToString();

        targetVolumeText.text = scoreManager.TargetVolume.ToString();

        // Update the volume text
        UpdateVolumeText(audioManager.CurrentVolumeDb);
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