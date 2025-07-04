using System.Collections;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int Score { get; private set; }
    public AudioManager audioManager;
    public int TargetVolume { get; private set; }

    private static readonly Timer GameOverCountdown = new();

    private const float Tolerance = 5f;

    void Start()
    {
        StartCoroutine(UpdateScore());
        StartCoroutine(GenerateTargetVolume());

        GameOverCountdown.Elapsed += GameOver;
        GameOverCountdown.Interval = 10000; // ~ 5 seconds
        GameOverCountdown.Enabled = true;
    }
    
    void OnDestroy()
    {
        GameOverCountdown.Elapsed -= GameOver;
        GameOverCountdown.Stop();
        GameOverCountdown.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        if (Score < 0)
        {
            GameOver();
        }
    }

    IEnumerator GenerateTargetVolume()
    {
        while(true)
        {
            TargetVolume = Random.Range(-30, -2);
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator UpdateScore()
    {
        while (true)
        {
            if (Mathf.Abs(TargetVolume - audioManager.CurrentVolumeDb) < Tolerance)
            {
                Score++;
                GameOverCountdown.Interval = 10000; // ~ 5 seconds
            }
            else
            {
                Score--;
            }

            yield return new WaitForSeconds(1);
        }
    }

    void GameOver(object sender, ElapsedEventArgs e)
    {
        GameOver();
    }
    
    void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadSceneAsync("GameOver");
    }
}


/*
every five seconds, a new target volume is generated.

every second the player volume is within +- 2.5 dB of target, the score is increased by 1.

the score goes down by 5 every second the player volume is outside the target.
OR
the game ends after 5 seconds of being outside the target volume.
*/