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

    public GameObject mainMenuCanvas; // Reference to the Main Menu Canvas
    public GameObject optionsCanvas;  // Reference to the Options Canvas

    private string startEnglish = "PLAY";
    private string startGerman = "SPIEL STARTEN";
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

        // Ensure the Main Menu is visible and the Options Menu is hidden at start
        mainMenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
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
        SceneManager.LoadScene("Airport"); // Load the Airport scene
    }

    public void Options()
    {
        // Hide the Main Menu and show the Options Menu
        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void GoBackToMainMenu()
    {
        // Hide the Options Menu and show the Main Menu
        optionsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit(); // Quit the application
    }
}