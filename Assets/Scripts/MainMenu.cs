using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private IEnumerator Coroutine;
    public Button StartButton;
    public Button ExitButton;
    public AudioSource StartGameSound;
    public Image Background;
    public Image LeftEye;
    public Image RightEye;
    public bool StartFading = false;
    public bool ShowEyes = false;

    void Awake()
    {
        StartGameSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (StartFading == true)
        {
            Color TempAlpha = Background.color;
            TempAlpha.a -= (0.1f * Time.deltaTime);
            Background.color = TempAlpha;

            if (TempAlpha.a <= 0)
            {
                ShowEyes = true;
                StartFading = false;
            }
        }
        if (ShowEyes == true)
        {
            Color TempAlpha = LeftEye.color;
            TempAlpha.a += (0.5f * Time.deltaTime);
            LeftEye.color = TempAlpha;
            RightEye.color = TempAlpha;

            if (TempAlpha.a >= 1f)
            {
                ShowEyes = false;
            }
        }
    }

    public void StartGame()
    {
        if (StartButton.GetComponent<Button>().enabled == true)
        {
            StartButton.GetComponent<Button>().enabled = false;
        }
        if (ExitButton.GetComponent<Button>().enabled == true)
        {
            ExitButton.GetComponent<Button>().enabled = false;
        }

        StartFading = true;
        StartGameSound.Play(0);
        Coroutine = WaitThenLoad(25f);
        StartCoroutine(Coroutine);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitThenLoad(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadScene("MovementTest");
    }
}
