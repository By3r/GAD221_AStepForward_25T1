using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public TMP_Text startButtonText;
    public TMP_Text creditsButtonText;
    public TMP_Text quitButtonText;

    public GameObject mainMenuCanvas; // Reference to the Main Menu Canvas
    public GameObject creditsCanvas;  // Reference to the Credits Canvas

    private string startEnglish = "PLAY";
    private string startGerman = "SPIEL STARTEN";
    private string creditsEnglish = "CREDITS";
    private string creditsGerman = "CREDITS";
    private string quitEnglish = "QUIT";
    private string quitGerman = "AUFHÖREN";

    void Start()
    {
        Cursor.visible = true;

        // Initialize button texts
        startButtonText.text = startEnglish;
        creditsButtonText.text = creditsEnglish;
        quitButtonText.text = quitEnglish;

        // Ensure the Main Menu is visible and the Options Menu is hidden at start
        mainMenuCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

    public void OnStartButtonHoverEnter()
    {
        startButtonText.text = startGerman;
    }

    public void OnStartButtonHoverExit()
    {
        startButtonText.text = startEnglish;
    }

    public void OnCreditsButtonHoverEnter()
    {
        creditsButtonText.text = creditsGerman;
    }

    public void OnOptionsButtonHoverExit()
    {
        creditsButtonText.text = creditsEnglish;
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
        SceneManager.LoadScene(1); // Load the Airport scene
    }

    public void Options()
    {
        // Hide the Main Menu and show the Options Menu
        mainMenuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }

    public void GoBackToMainMenu()
    {
        // Hide the Options Menu and show the Main Menu
        creditsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit(); // Quit the application
    }
}