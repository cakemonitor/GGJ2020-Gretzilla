using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public float startDelay = 22.0f;

    void Start()
    {
        Invoke("StartMusic", startDelay);
    }

    void StartMusic()
    {
        musicAudioSource.Play();
    }
}
