using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private IEnumerator Coroutine_LoadGame;
    private IEnumerator Coroutine_ShowEyes;

    public Button StartButton;
    public Button ExitButton;
    public AudioSource StartGameSound;
    public Image Background;
    public Image Face;
    public Image GlowingEyes;
    public bool StartFading = false;
    public bool ShowFace = false;
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
            StartButton.GetComponent<Image>().color = TempAlpha;
            StartButton.GetComponentInChildren<Image>().color = TempAlpha;
            ExitButton.GetComponent<Image>().color = TempAlpha;
            ExitButton.GetComponentInChildren<Image>().color = TempAlpha;

            if (TempAlpha.a <= 0)
            {
                ShowFace = true;
                StartFading = false;
            }
        }
        if (ShowFace == true)
        {
            Color TempAlpha = Face.color;
            TempAlpha.a += (0.1f * Time.deltaTime);
            Face.color = TempAlpha;

            if (TempAlpha.a >= 0.6f)
            {
                Coroutine_ShowEyes = WaitAndShow(1.5f);
                StartCoroutine(Coroutine_ShowEyes);
                ShowFace = false;
            }
        }
        if (ShowEyes == true)
        {
            Color TempAlpha = GlowingEyes.color;
            TempAlpha.a += (3f * Time.deltaTime);
            GlowingEyes.color = TempAlpha;

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
        Coroutine_LoadGame = WaitThenLoad(25f);
        StartCoroutine(Coroutine_LoadGame);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitAndShow(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        ShowEyes = true;
    }

    private IEnumerator WaitThenLoad(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadScene("MovementTest");
    }
}
