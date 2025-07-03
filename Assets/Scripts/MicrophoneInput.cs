using UnityEngine;

public class MicrophoneInput : MonoBehaviour
{
    public AudioClip micClip;
    public int sampleLength = 1024;
    public float[] samples;
    int micSampleRate;
    string micName;

    void Start()
    {
        samples = new float[sampleLength];
        micName = Microphone.devices[0];
        micSampleRate = AudioSettings.outputSampleRate;

        micClip = Microphone.Start(micName, true, 10, micSampleRate);
    }

    void Update()
    {
        if (!Microphone.IsRecording(micName)) return;
        int micPos = Microphone.GetPosition(micName);
        int startPos = micPos - sampleLength;

        if (startPos < 0) return;

        micClip.GetData(samples, startPos);
    }
}