using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public TMP_Text startButtonText;
    public TMP_Text optionsButtonText;
    public TMP_Text quitButtonText;

    private string startEnglish = "START";
    private string startGerman = "der Start";
    private string optionsEnglish = "OPTIONS";
    private string optionsGerman = "OPTIONEN";
    private string quitEnglish = "QUIT";
    private string quitGerman = "Aufhören";

    void Start()
    {
        // Initialize button texts
        startButtonText.text = startEnglish;
        optionsButtonText.text = optionsEnglish;
        quitButtonText.text = quitEnglish;
    }

    public void OnStartButtonHoverEnter()
    {
        startButtonText.text = startGerman;
    }

    public void OnStartButtonHoverExit()
    {
        startButtonText.text = startEnglish;
    }

    public void OnOptionsButtonHoverEnter()
    {
        optionsButtonText.text = optionsGerman;
    }

    public void OnOptionsButtonHoverExit()
    {
        optionsButtonText.text = optionsEnglish;
    }

    public void OnQuitButtonHoverEnter()
    {
        quitButtonText.text = quitGerman;
    }

    public void OnQuitButtonHoverExit()
    {
        quitButtonText.text = quitEnglish;
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Airport"); // Load Level 1 scene
    }

    public void Options()
    {
        SceneManager.LoadSceneAsync("Options", LoadSceneMode.Additive); // Load Options scene additively
    }

    public void Quit()
    {
        Application.Quit(); // Quit the application
    }
}
