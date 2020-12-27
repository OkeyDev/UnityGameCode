using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioType[] audioClips;
    private AudioSource audioSource;
    int clipIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < audioClips.Length; i++) {
            audioClips[i].SetAverage();
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0].audioClip;
        audioSource.Play();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnd(audioClips[clipIndex].audioClip)) {
            SetNextClip();
        }   
    }
    /// <summary>
    /// Check if clip is end
    /// </summary>
    /// <param name="clip">clip, which playing</param>
    /// <returns>bool, is clip end</returns>
    bool isEnd (AudioClip clip) {
        return audioSource.time == clip.length;
    }
    /// <summary>
    /// Set next clip on audio surce
    /// </summary>
    void SetNextClip() {
        clipIndex = (clipIndex + 1) % audioClips.Length;
        audioSource.clip = audioClips[clipIndex].audioClip;
        audioSource.Play();
    }
    /// <summary>
    /// return amplitude of playing track
    /// </summary>
    /// <returns>float, amplitude of playing track</returns>
    public float GetAmplitude() {
        return audioClips[clipIndex].GetAmplitude(audioSource.timeSamples);
    }
}
