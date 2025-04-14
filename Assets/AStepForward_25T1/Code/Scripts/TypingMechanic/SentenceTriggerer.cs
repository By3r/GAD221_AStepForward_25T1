using TMPro;
using UnityEngine;

public class SentenceTriggerer : MonoBehaviour
{
    #region Variables
    [SerializeField] private Sentences _sentenceToDisplay;
    [SerializeField] private SentenceValidator _sentenceValidator;
    [SerializeField] private TMP_Text sentenceTextDisplay;
    [SerializeField] private GameObject taskDifficultyPanel;
    [SerializeField] private GameObject taskOnGoingPanel;

    private bool _hasLoadedSentences = false;

    private bool _tempPlayerAttemptedGamePlay = false;
    #endregion

    private void Update()
    {
        LoadSentences();
        if (sentenceTextDisplay.text == "")
        {
            taskOnGoingPanel.SetActive(false);
        }
    }

    public void LoadSentences()
    {
        if (taskDifficultyPanel.activeSelf && !_hasLoadedSentences)
        {
            _sentenceValidator.LoadSentenceSet(_sentenceToDisplay);
            _hasLoadedSentences = true;
        }
        else if (!taskDifficultyPanel.activeSelf)
        {
            _hasLoadedSentences = false;
        }
    }

    #region Mouse Enter&Exit
    private void OnMouseEnter()
    {
        if (!_tempPlayerAttemptedGamePlay)
        {
            taskDifficultyPanel.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        Invoke("SetPanelAsInactive", 3);
    }

    private void SetPanelAsInactive()
    {
        taskDifficultyPanel.SetActive(false);
    }
    #endregion
}
