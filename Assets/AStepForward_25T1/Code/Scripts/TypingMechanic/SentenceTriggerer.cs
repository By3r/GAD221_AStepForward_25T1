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
    public GameObject GetOngoingPanel() => taskOnGoingPanel;

    [SerializeField] private GameObject exclamationMarkIcon;
    [SerializeField] private NPCDialogue assignedNPCDialogue;

    public NPCDialogue GetNPCDialogue() => assignedNPCDialogue;

    private bool _hasLoadedSentences = false;
    private bool _canPressEnterToConfirm = false;
    private bool _taskStarted = false;
    private bool _taskCompleted = false;

    private const int DefaultLayer = 0;
    private const int TaskLayer = 6;
    #endregion

    private void Update()
    {
        if (_taskCompleted) return;

        if (_canPressEnterToConfirm && !_taskStarted && Input.GetKeyDown(KeyCode.Return))
        {
            StartTask();
        }
    }

    private void OnMouseEnter()
    {
        if (_taskCompleted) return;

        if (!_hasLoadedSentences)
        {
            taskDifficultyPanel.SetActive(true);
            LoadSentences();
        }
    }

    private void OnMouseExit()
    {
        if (_taskCompleted) return;

        Invoke(nameof(SetPanelAsInactive), 3);

        _sentenceValidator?.HideSentenceTranslation();
        assignedNPCDialogue?.HideTranslation();
    }

    private void StartTask()
    {
        _sentenceValidator.LoadSentenceSet(_sentenceToDisplay, this);
        taskOnGoingPanel.SetActive(true);
        _hasLoadedSentences = true;
        _taskStarted = true;
        _canPressEnterToConfirm = false;
    }

    public void LoadSentences()
    {
        if (taskDifficultyPanel.activeSelf && !_hasLoadedSentences)
        {
            _canPressEnterToConfirm = true;
        }
        else if (!taskDifficultyPanel.activeSelf)
        {
            _hasLoadedSentences = false;
            _canPressEnterToConfirm = false;
        }
    }

    private void SetPanelAsInactive()
    {
        taskDifficultyPanel.SetActive(false);
    }

    public void OnTaskCompleted()
    {
        _taskCompleted = true;
        _taskStarted = false;

        if (taskOnGoingPanel != null)
            taskOnGoingPanel.SetActive(false);

        gameObject.layer = DefaultLayer;

        if (exclamationMarkIcon != null)
            exclamationMarkIcon.SetActive(false);

        Debug.Log($"Task {_sentenceToDisplay.name} completed");
    }

    public void OnTaskFailed()
    {
        _taskCompleted = false;
        _taskStarted = false;

        gameObject.layer = TaskLayer;

        if (exclamationMarkIcon != null)
            exclamationMarkIcon.SetActive(true);

        Debug.Log($"Task {_sentenceToDisplay.name} failed");
    }
}
