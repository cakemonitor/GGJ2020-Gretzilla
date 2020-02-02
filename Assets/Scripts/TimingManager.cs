using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public GameObject gretzillaModel;
    public float startScale;
    public float startGrowthTime = 5.0f;
    public float fullSizeTime = 7.0f;
    public AudioSource musicAudioSource;
    public float musicStartDelay = 22.0f;
    PlayerController playerController;
    CameraController cameraController;

    void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.enabled = false;
        playerController = gretzillaModel.transform.parent.GetComponent<PlayerController>();
        playerController.enabled = false;
    }

    void Start()
    {
        gretzillaModel.transform.localScale = Vector3.zero;
        Invoke("StartGrowth", startGrowthTime);
        Invoke("GameStart", musicStartDelay);
    }

    void GameStart()
    {
        cameraController.enabled = true;
        playerController.enabled = true;
        musicAudioSource.Play();
    }

    void StartGrowth()
    {
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        float scaleFactor = startScale;
        float growthPerSecond = 1.0f / (fullSizeTime - startGrowthTime);
        while (scaleFactor < 1.0)
        {
            scaleFactor += growthPerSecond * Time.deltaTime;
            gretzillaModel.transform.localScale = Vector3.one * scaleFactor;
            yield return null;
        }
        gretzillaModel.transform.localScale = Vector3.one;
    }
}
