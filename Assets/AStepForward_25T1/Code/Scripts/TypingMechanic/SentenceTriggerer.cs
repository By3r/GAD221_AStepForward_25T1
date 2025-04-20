using UnityEngine;

public class SentenceTriggerer : MonoBehaviour
{
    #region Variables
    public NPCDialogue GetNPCDialogue() => assignedNPCDialogue;
    public GameObject GetOngoingPanel() => taskOngoingPanel;

    [SerializeField] private Sentences _sentenceToDisplay;
    [SerializeField] private SentenceValidator _sentenceValidator;
    [SerializeField] private GameObject taskDifficultyPanel;
    [SerializeField] private GameObject taskOngoingPanel;
    [SerializeField] private GameObject exclamationMarkIcon;
    [SerializeField] private NPCDialogue assignedNPCDialogue;

    private bool _hasLoadedSentences = false;
    private bool _taskStarted = false;
    private bool _taskCompleted = false;
    #endregion

    private void OnEnable()
    {
        GameEvents.OnTaskCompleted += HandleTaskCompleted;
        GameEvents.OnTaskFailed += HandleTaskFailed;
    }

    private void OnDisable()
    {
        GameEvents.OnTaskCompleted -= HandleTaskCompleted;
        GameEvents.OnTaskFailed -= HandleTaskFailed;
    }

    private void OnMouseDown()
    {
        if (_taskCompleted || _hasLoadedSentences) return;

        taskDifficultyPanel.SetActive(true);
        _hasLoadedSentences = true;

        GameEvents.OnTaskRetryRequested?.Invoke(_sentenceToDisplay);
    }

    #region Public Functions
    public void StartTaskButtonPressed()
    {
        if (_taskStarted || _taskCompleted) return;

        _taskStarted = true;
        _hasLoadedSentences = true;
        taskDifficultyPanel.SetActive(false);
        taskOngoingPanel.SetActive(true);

        _sentenceValidator.LoadSentenceSet(_sentenceToDisplay, this);
        GameEvents.OnTaskStarted?.Invoke(_sentenceToDisplay);
    }

    public void CancelTaskPanel(GameObject taskpanel)
    {
        _taskCompleted = false;
        taskpanel.SetActive(false);
        _hasLoadedSentences = false;
    }
    #endregion

    #region Private Functions
    private void HandleTaskCompleted(Sentences task)
    {
        if (task != _sentenceToDisplay) return;

        _taskCompleted = true;
        _taskStarted = false;

        if (taskOngoingPanel != null)
            taskOngoingPanel.SetActive(false);

        if (exclamationMarkIcon != null)
            exclamationMarkIcon.SetActive(false);

        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void HandleTaskFailed(Sentences task)
    {
        if (task != _sentenceToDisplay) return;

        _taskStarted = false;
        _taskCompleted = false;
        _hasLoadedSentences = false;

        if (exclamationMarkIcon != null) exclamationMarkIcon.SetActive(true);
    }
    #endregion
}