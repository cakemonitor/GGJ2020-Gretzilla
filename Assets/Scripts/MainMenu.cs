using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private IEnumerator Coroutine;
    public AudioSource StartGameSound;

    public void Awake()
    {
        StartGameSound = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        StartGameSound.Play(0);
        Coroutine = WaitThenLoad(25f);
        StartCoroutine(Coroutine);
    }

    private IEnumerator WaitThenLoad(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadScene("MovementTest");
    }
}
