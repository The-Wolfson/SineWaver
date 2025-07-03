using UnityEngine;

public class MicrophoneInput : MonoBehaviour
{
    public AudioClip micClip;
    public int sampleLength = 1024;
    public float[] samples;
    private int _micSampleRate;
    private string _micName;

    private void Start()
    {
        samples = new float[sampleLength];
        _micName = Microphone.devices[0];
        _micSampleRate = AudioSettings.outputSampleRate;

        micClip = Microphone.Start(_micName, true, 10, _micSampleRate);
    }

    private void Update()
    {
        if (!Microphone.IsRecording(_micName)) return;
        var micPos = Microphone.GetPosition(_micName);
        var startPos = micPos - sampleLength;

        if (startPos < 0) return;

        micClip.GetData(samples, startPos);
    }
}