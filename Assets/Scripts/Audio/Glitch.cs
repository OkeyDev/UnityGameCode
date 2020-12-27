using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
/// <summary>
/// Use for glitch effect
/// </summary>
public class Glitch : MonoBehaviour
{
    public MusicController musicController;
    public AudioSource audioSource;
    public PostProcessVolume volume;
    private PostProcessProfile profile;
    private ChromaticAberration glitch;
    private static bool isExist = false;
    void Start()
    {
        // TODO: do it normal
        // if audio and glitch effect already exist, destroy it
        if (isExist) { 
            Destroy(musicController.gameObject);
            Destroy(volume.gameObject);
        }
        isExist = true;
        profile = volume.profile;
        profile.TryGetSettings<ChromaticAberration>(out glitch);
        // save on change scene
        DontDestroyOnLoad(musicController.gameObject);
        DontDestroyOnLoad(volume.gameObject);
    }
    void Update()
    {
        glitch.intensity.value = musicController.GetAmplitude(); // set glitch eddect
    }
}
