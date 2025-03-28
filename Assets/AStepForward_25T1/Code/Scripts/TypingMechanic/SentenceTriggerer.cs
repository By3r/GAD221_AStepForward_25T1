using UnityEngine;

public class SentenceTriggerer : MonoBehaviour
{
    #region Variables
    [SerializeField] private Sentences _sentenceToDisplay;
    [SerializeField] private SentenceValidator _sentenceValidator;
    [SerializeField] private GameObject taskDifficultyPanel;

    private bool _hasLoadedSentences = false;
    #endregion

    void Update()
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
        taskDifficultyPanel.SetActive(true);
    }

    private void OnMouseExit()
    {
        taskDifficultyPanel.SetActive(false);
    }
    #endregion
}
