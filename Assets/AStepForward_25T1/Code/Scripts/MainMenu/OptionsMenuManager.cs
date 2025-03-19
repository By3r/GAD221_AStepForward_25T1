using UnityEngine;
using TMPro;

public class OptionsMenuManager : MonoBehaviour
{
    public TMP_Text backButtonText;

    private string backEnglish = "BACK";
    private string backGerman = "Zurück";

    void Start()
    {
        // Initialize button text
        backButtonText.text = backEnglish;
    }

    public void OnBackButtonHoverEnter()
    {
        backButtonText.text = backGerman;
    }

    public void OnBackButtonHoverExit()
    {
        backButtonText.text = backEnglish;
    }

    public void GoBackToMainMenu()
    {
        // Call the MainMenuManager's GoBackToMainMenu method
        MainMenuManager mainMenuManager = FindObjectOfType<MainMenuManager>();
        if (mainMenuManager != null)
        {
            mainMenuManager.GoBackToMainMenu();
        }
    }
}