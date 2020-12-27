using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AudioType 
{
    public AudioClip audioClip;
    private float averageAmplitude;
    public void SetAverage () {
        // So... I tried to do glitch based on music style...
        /*
        float average = 0;
        float[] audioSamples = new float[1024]; 
        audioClip.GetData(audioSamples, audioClip.samples/2);
        for (int i = 0; i < audioSamples.Length; i++) {
            average += audioSamples[i];
        }
        average /= audioSamples.Length; */
        averageAmplitude = 0.5f;
    }
    public float GetAverageAmplitude () {
        return averageAmplitude;
    }
    /// <summary>
    /// Return audio amlitude of selected audio
    /// </summary>
    /// <param name="timeSamples">audio offset; you can get it on AudioSource.timeSamples</param>
    /// <returns>audio amplitude</returns>
    public float GetAmplitude(int timeSamples) {
        float[] data = new float[1 * audioClip.channels];
        audioClip.GetData(data, timeSamples);
        return Average(data);
    }
    /// <summary>
    /// select an arifmetic average
    /// </summary>
    /// <param name="arr">array of floats</param>
    /// <returns>arifmetic average</returns>
    float Average (float[] arr) {
        float n = 0;
        for (int i = 0; i < arr.Length; i++) {
            n += Mathf.Abs(arr[i]);
        }
        return n/arr.Length;
    }
}
