using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}


/*
every five seconds, a new target volume is generated.

every second the player volume is within +- 2.5 dB of target, the score is increased by 1.

the score goes down by 5 every second the player volume is outside the target.
OR
the game ends after 5 seconds of being outside the target volume.
*/